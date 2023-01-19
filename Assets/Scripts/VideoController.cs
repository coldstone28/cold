using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UI.SaveSystem;
using UnityEngine.Video;



public class VideoController : MonoBehaviour
{
    enum AspectRatio
    {
        Square, TwoToOne
    };

    public RenderTexture renderTextureSquare;
    public RenderTexture renderTexture2to1;
    
    public Material SkyBox2to1;
    public Material SkyBoxSquare;
    private AspectRatio currentAspectRatio = AspectRatio.Square;

    private VideoPlayer videoPlayer;
    private List<VideoPlayer> bufferVideoPlayerList = new List<VideoPlayer>();
    internal bool videoIsLooping;
    private UIController uiController;
    private ProjectController projectController;

    public UnityEvent onVideoPlaybackStarted;
    public UnityEvent onVideoPlaybackEnded;

    internal bool reachedEndAndStopped;

    private void Awake()
    {
        videoPlayer = gameObject.AddComponent(typeof(VideoPlayer)) as VideoPlayer;
        videoPlayer.waitForFirstFrame = true;
    }

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
        if (!uiController)
        {
            Debug.LogError("Error in VideoController: No uiController found in scene");
        }
        projectController = FindObjectOfType<ProjectController>();
        if (!projectController)
        {
            Debug.LogError("no project controller found in scene");
        }
        videoPlayer.source = VideoSource.Url;
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.playOnAwake = false;
        videoPlayer.targetTexture = renderTextureSquare;
        RenderSettings.skybox = SkyBoxSquare;
        videoPlayer.aspectRatio = VideoAspectRatio.FitHorizontally;
        videoPlayer.prepareCompleted += source => Play();
        SkyBoxSquare.SetInt("_Layout", 2);
        PlayDefaultVideo();
    }

    internal void AddToVideoBuffer(string videoFile)
    {
        //TODO: Buffering disabled (see @PlaySceneVideo)
        //StartCoroutine(AddToVideoBufferAsync(videoFile));
    }
    
    private IEnumerator AddToVideoBufferAsync(string videoFile)
    {
        VideoPlayer bufferPlayer = gameObject.AddComponent(typeof(VideoPlayer)) as VideoPlayer;
        bufferPlayer.Stop();
        bufferPlayer.playOnAwake = false;
        bufferPlayer.waitForFirstFrame = true;
        bufferPlayer.loopPointReached += EndReached;
        bufferPlayer.source = VideoSource.Url;
        bufferPlayer.aspectRatio = VideoAspectRatio.FitHorizontally;
        bufferPlayer.url = Application.streamingAssetsPath + "/" + videoFile;
        bufferPlayer.Prepare();
        bufferVideoPlayerList.Add(bufferPlayer);
        yield return null;
    }

    internal void ClearVideoBuffer()
    {
        foreach (var bufferVideo in bufferVideoPlayerList)
        {
            bufferVideo.Stop();
            Destroy(bufferVideo);
        }
        bufferVideoPlayerList.Clear();
    }
    
    internal void PlaySceneVideo(string videoFile)
    {
        bool isVideoInBuffer = false;
        //TODO: Buffering system crashes app on oculus - may be a ram or cpu issue, have to figure that out
        /*foreach (var bufferPlayer in bufferVideoPlayerList)
        {
            if (bufferPlayer.url.Equals(Application.streamingAssetsPath + "/" + videoFile))
            {
                isVideoInBuffer = true;
                VideoPlayer oldPlayer = videoPlayer;
                videoPlayer = bufferPlayer;
                oldPlayer.targetTexture = null;
                oldPlayer.Stop();
                Destroy(oldPlayer);
            }
        }*/
        if (!isVideoInBuffer)
        {
            videoPlayer.url = ProjectLoader.VIDEOPOOL_DIR_PATH + "/" + videoFile;
        }
        else
        {
            bufferVideoPlayerList.Remove(videoPlayer);
        }
        //ClearVideoBuffer();
        videoPlayer.isLooping = videoIsLooping;
        startPlayingFromZero();
    }

    //TODO: So far pauses on first frame, because last frame couldn't be calculated, yet
    internal void PlayStreamingVideoOnLastFrame(string videoFile)
    {
        PlaySceneVideo(videoFile);
        if (!videoIsLooping)
        {
            onVideoPlaybackStarted.AddListener(pauseVideoOnPlaybackStart);
        }
    }

    
    private void updateRenderTexture()
    {
        //if src video resolution/aspect ratio is nearly or exact 1:1, use the square render texture
        if(videoPlayer.texture.height <= videoPlayer.texture.width + 50 && videoPlayer.texture.height >= videoPlayer.texture.width - 50)
        {
            if (currentAspectRatio != AspectRatio.Square)
            {
                if ( videoPlayer.targetTexture != null ) {
                    videoPlayer.targetTexture.Release( );
                }
                videoPlayer.targetTexture = renderTextureSquare;
                RenderSettings.skybox = SkyBoxSquare;
                currentAspectRatio = AspectRatio.Square;
            }
        }
        else
        {
            if (currentAspectRatio != AspectRatio.TwoToOne)
            {
                if ( videoPlayer.targetTexture != null ) {
                    videoPlayer.targetTexture.Release( );
                }
                videoPlayer.targetTexture = renderTexture2to1;
                RenderSettings.skybox = SkyBox2to1;
                currentAspectRatio = AspectRatio.TwoToOne;
            }
        }
    }
    
    private void pauseVideoOnPlaybackStart()
    {
        videoPlayer.Pause();
        onVideoPlaybackStarted.RemoveListener(pauseVideoOnPlaybackStart);
    }

    private void PlayDefaultVideo()
    {
        videoPlayer.url = Application.streamingAssetsPath + "/base_scene.mp4";
        startPlayingFromZero();
    }

    internal void startPlayingFromZero()
    {
        videoPlayer.Stop();
        if (!videoPlayer.isLooping && !projectController.authoringMode)
        {
            uiController.hideInteractionSphere();
        }
        else
        {
            uiController.showInteractionSphere();
        }
        videoPlayer.Prepare();
    }

    void Play()
    {
        updateRenderTexture();
        reachedEndAndStopped = false;
        videoPlayer.Play();
        onVideoPlaybackStarted.Invoke();
    }
    
    void EndReached(VideoPlayer vp)
    {
        if (!videoPlayer.isLooping && !projectController.authoringMode) {
            uiController.showInteractionSphere(); //important only if interaction sphere was hidden during playback
        }

        if (!videoPlayer.isLooping)
        {
            reachedEndAndStopped = true;
        }
        onVideoPlaybackEnded.Invoke();
    }

    //obsolete
    IEnumerator waitForPlayback()
    {
        yield return new WaitUntil(() => videoPlayer.frame > 0);
        onVideoPlaybackStarted.Invoke();
    }

    internal void StopCurrentVideo()
    {
        PlayDefaultVideo();
    }

    public void setIsVideoLoopingWithReloadCheck(bool looping)
    {
        videoIsLooping = looping;
        videoPlayer.isLooping = looping;
        if (!looping && reachedEndAndStopped)
        {
            startPlayingFromZero();
        }
    }
    
    public void setIsVideoLooping(bool looping)
    {
        videoIsLooping = looping;
        videoPlayer.isLooping = looping;
    }

    public void toggleStereoMode(bool isStereo)
    {
        if (isStereo)
        {
            SkyBoxSquare.SetInt("_Layout", 2);
        }
        else
        {
            SkyBoxSquare.SetInt("_Layout", 3);
        }
    }
}
