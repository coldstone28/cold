using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.SaveSystem;
using UnityEngine.Video;

public class AssetLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    internal string[] videoFiles;
    
    void Start()
    {
        if (!videoPlayer)
        {
            enabled = false;
        }
        else
        {
            videoFiles = getAllVideoFiles();
        }
    }

    internal string[] getAllVideoFiles()
    {
        string[] files = Directory.GetFiles(ProjectLoader.VIDEOPOOL_DIR_PATH, "*.mp4");
        return files.Select(file => Path.GetFileName(file)).ToArray();
    }
    
    internal string[] getAllPngImageFiles()
    {
        string[] files = Directory.GetFiles(ProjectLoader.IMAGEPOOL_DIR_PATH, "*.png");
        return files.Select(file => Path.GetFileName(file)).ToArray();
    }
    
    internal string[] getAllAudioFiles()
    {
        string[] files = Directory.GetFiles(ProjectLoader.AUDIOPOOL_DIR_PATH, "*.mp3");
        string[] fileswav = Directory.GetFiles(ProjectLoader.AUDIOPOOL_DIR_PATH, "*.wav");
        string[] result = new string[files.Length + fileswav.Length];
        Array.Copy(files, result, files.Length);
        Array.Copy(fileswav, 0, result, files.Length, fileswav.Length);
        return result.Select(file => Path.GetFileName(file)).ToArray();
    }

    internal byte[] getBytesFromImageFile(string imageFile)
    {
        return File.ReadAllBytes(ProjectLoader.IMAGEPOOL_DIR_PATH + "/" + imageFile);
    }
}
