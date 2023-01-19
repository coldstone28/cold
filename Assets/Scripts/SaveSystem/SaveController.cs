using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UI.SaveSystem;
using Utilities;

public class SaveController : MonoBehaviour
{
    internal static string FAIL_SCENE_TRIGGER = "sys_sc_failed";
    internal static string SUCCESS_SCENE_TRIGGER = "sys_sc_success";
    private SceneScanner sceneScanner;
    private SceneLoader sceneLoader;
    private ProjectController projectController;
    private VideoController videoController;
    internal UIController uiController;
    internal UnityEvent OnSceneVideoEndReached = new UnityEvent();
    internal bool isLoadingScene = false;
    
    void Start()
    {
        sceneScanner = FindObjectOfType<SceneScanner>();
        if (!sceneScanner)
        {
            Debug.LogError("no scene scanner found in scene");
        }
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader)
        {
            Debug.LogError("no scene loader found in scene");
        }
        projectController = FindObjectOfType<ProjectController>();
        if (!projectController)
        {
            Debug.LogError("no project controller found in scene");
        }
        uiController = FindObjectOfType<UIController>();
        if (!uiController)
        {
            Debug.LogError("no UI controller found in scene");
        }
        videoController = FindObjectOfType<VideoController>();
        if (!videoController)
        {
            Debug.LogError("Error in SceneLoader: Couldn't find videoController in scene");
        }
        videoController.onVideoPlaybackEnded.AddListener(delegate
        {
            OnSceneVideoEndReached.Invoke();
        });
        videoController.onVideoPlaybackStarted.AddListener(unlockSceneLoading);
    }

    private void deleteSaveDataInActiveProject()
    {
        DirectoryInfo di = new DirectoryInfo(projectController.activeProjectPath);
        FileInfo[] files = di.GetFiles("*.json").Where(p => p.Extension == ".json").ToArray();
        foreach (FileInfo file in files)
        {
            try
            {
                file.Attributes = FileAttributes.Normal;
                File.Delete(file.FullName);
            }
            catch
            {
            }
        }
    }

    public void updateCurrentSceneData()
    {
        SceneData sceneData = sceneScanner.scanCurrentScene(projectController.activeScene);
        SaveIntoJson(sceneData);
        projectController.setNewSaveTimeStamp();
        projectController.OnProjectsChanged.Invoke();
    }

    public void renameCurrentScene(string sceneName)
    {
        sceneLoader.renameSceneListFile(sceneScanner.getCurrentSceneName(), sceneName);
        RemoveJsonSceneData(sceneScanner.getCurrentSceneName());
        sceneLoader.setSceneName(sceneName);
        updateCurrentSceneData();
    }

    private SceneData createEmptySceneData(string sceneName)
    {
        SceneData newSceneData = new SceneData();
        newSceneData.sceneName = sceneName;
        newSceneData.interactionSphereSize = 750;
        newSceneData.videoIsLooping = 1;
        return newSceneData;
    }

    /// <summary>
    /// Creates a new scene and adds it to scene list, without loading it
    /// </summary>
    public void createNewScene(string name)
    {
        SceneData newScene = createEmptySceneData(name);
        sceneLoader.addToSceneList(name);
        SaveIntoJson(newScene);
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("SceneList"))
        {
            gameObject.GetComponent<SceneList>().reloadScenes();
        }
    }
    
    /// <summary>
    /// opens an input field for creating a new scene with a custom name
    /// </summary>
    public void createNewScene()
    {
        uiController.openGenericInputField(createNewScene);
    }
    
    /// <summary>
    /// Creates a new scene, adds it to scene list and loads it
    /// </summary>
    public void loadNewScene(string name)
    {
        createNewScene(name);
        loadScene(name);
    }

    /// <summary>
    /// If sceneName is a sys_trigger string, interactable handles that instead of loading a new scene.
    /// /// </summary>
    public void handleSysTrigger(TargetScene scene, Base360Interactable triggeringInteractable)
    {
        if(scene.targetSceneName.Equals(FAIL_SCENE_TRIGGER))
        {
            uiController.showWarningFold();
            uiController.openGenericVerifyField(resumeScenarioAfterEnd, scene.hiddenData);
        }
        else if(scene.targetSceneName.Equals(SUCCESS_SCENE_TRIGGER))
        {
            uiController.showSuccessFold();
            uiController.openGenericVerifyField(resumeScenarioAfterEnd, scene.hiddenData);
        }
    }

    /// <summary>
    /// Loads the scene with given name in current project folder.
    /// Creates and loads an new scene with this name, if no scene could be found.
    /// Seperate method overload for working with dynamic unity events.
    /// </summary>
    public void loadScene(string name)
    {
        loadScene(name, false);
    }
    
    /// <summary>
    /// Loads the scene with given name in current project folder.
    /// Creates and loads an new scene with this name, if no scene could be found.
    /// </summary>
    public void loadScene(string name, bool isInitialScene = false)
    {
        if (!isLoadingScene)
        {
            videoController.reachedEndAndStopped = false; //TODO: Not a good solution to set it like this in terms of architecture
            isLoadingScene = true;
            SceneData sceneData = LoadFromJson(name);
            if (sceneData != null)
            {
                sceneLoader.LoadScene(sceneData, isInitialScene);
                projectController.activeScene = sceneData;
            }
            else
            {
                Debug.Log("creating new scene");
                loadNewScene(name);
            }
        }
    }

    /// <summary>
    /// Loads the scene video with given name in current project folder and returns after video reached end frame.
    /// </summary>
    public void loadSceneVideoTemporarily(string name)
    {
        SceneData currentSceneData = LoadFromJson(projectController.activeScene.sceneName);
        SceneData sceneData = LoadFromJson(name);
        if (sceneData != null)
        {
            UnityAction eventAction = null;
            eventAction = new UnityAction(delegate
            {
                videoController.PlayStreamingVideoOnLastFrame(currentSceneData.videoFileName);
                videoController.onVideoPlaybackEnded.RemoveListener(eventAction);
            });
            videoController.PlaySceneVideo(sceneData.videoFileName);
            videoController.onVideoPlaybackEnded.AddListener(eventAction);
        }
    }

    internal SceneData getScene(string sceneName)
    {
        return LoadFromJson(sceneName);
    }
    
    /// <summary>
    /// Loads the first scene found regardless of name or firstScene config in project data.
    /// Creates a new scene called "neue_szene", if nothing could be found
    /// </summary>
    public void loadScene()
    {
        string[] sceneList = sceneLoader.getSceneList();
        if (sceneList.Any())
        {
            loadScene(sceneList[0]);
        }
        else
        {
            loadScene("neue_szene");
        }
    }

    private void unlockSceneLoading()
    {
        isLoadingScene = false;
    }
    
    private void resumeScenarioAfterEnd(bool userConfirmed = true)
    {
        uiController.hideWarningFold();
        uiController.hideSuccessFold();
        if (!userConfirmed)
        {
            uiController.setProjectMenuVisibility(true);
        }
    }
    
    private void SaveIntoJson(SceneData sceneData){
        string sceneString = JsonUtility.ToJson(sceneData);
        File.WriteAllText(projectController.activeProjectPath + "/" + sceneData.sceneName + "Data.json", sceneString);
    }

    private void RemoveJsonSceneData(string sceneName)
    {
        File.Delete(projectController.activeProjectPath + "/" + sceneName + "Data.json");
    }
    
    private SceneData LoadFromJson(string sceneName)
    {
        SceneData scene;
        
        if(File.Exists(projectController.activeProjectPath + "/" + sceneName + "Data.json"))
        {
            string sceneString = File.ReadAllText(projectController.activeProjectPath + "/" + sceneName + "Data.json");
            scene = JsonUtility.FromJson<SceneData>(sceneString);
        }
        else
        {
            Debug.Log("No JSON file called " + sceneName + "Data.json in project folder");
            scene = null;
        }
        return scene;
    }
}
