using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace UnityEngine.UI.SaveSystem
{
    public class ProjectController : MonoBehaviour
    {
        public bool authoringMode { get; set; }
        
        public bool complexEditing { get; set; }
        
        internal static string SCENE_LIST_FILE = "sceneList.txt";
        
        internal static string REMOTE_PROJECT_LIST_FILE = "remoteProjectList.json";
        
        internal ProjectData activeProject { get; private set; }

        internal SceneData activeScene;
        
        internal string activeProjectPath;

        internal UIController uiController;

        internal SaveController saveController;

        private EditModeController editModeController;
        
        private ProjectLoader projectLoader;

        private ProjectUpdater remoteUpdater;
        
        private ImageBuffer ImageBuffer;

        public Button loadProjectButton;

        public Button newProjectButton;

        public Button delProjectButton;

        /// <summary>
        /// ProjectList component needs to wait until the projects folder exists, before loading
        /// </summary>
        internal UnityEvent OnProjectsChanged = new UnityEvent();

        private void Awake()
        {
            authoringMode = false;
            complexEditing = false;
            projectLoader = gameObject.AddComponent<ProjectLoader>();
            remoteUpdater = gameObject.AddComponent<ProjectUpdater>();
        }

        private void Start()
        {
            saveController = FindObjectOfType<SaveController>();
            if (!saveController)
            {
                Debug.LogError("Error in ProjectController: No save controller found in scene");
            }
            uiController = FindObjectOfType<UIController>();
            if (!uiController)
            {
                Debug.LogError("Error in ProjectController: No uiController found in scene");
            }
            editModeController = FindObjectOfType<EditModeController>();
            if (!editModeController)
            {
                Debug.LogError("Error in ProjectController: No editModeController found in scene");
            }
            saveController = FindObjectOfType<SaveController>();
            if (!saveController)
            {
                Debug.LogError("Error in ProjectController: No save controller found in scene");
            }

            ImageBuffer = FindObjectOfType<ImageBuffer>();
            if (!ImageBuffer)
            {
                Debug.LogError("Error in ProjectLoader: No imageBuffer found in scene");
            }
        }

        public void openNewProject(string projectName = "Neues Projekt")
        {
            ProjectData newProject = projectLoader.createNewProject(projectName);
            openProject(newProject.projectName);
        }
        
        /// <summary>
        /// loads project data with given name and opens first scene
        /// </summary>
        /// <param name="name">project name to open</param>
        internal void openProject(string name)
        {
            toggleMenuButtons(false);
            StartCoroutine(loadAndOpenProject(name));
        }

        private IEnumerator loadAndOpenProject(string name)
        {
            activeProject = projectLoader.loadProject(name);
            ImageBuffer.Initialize();
            
            yield return new WaitUntil(() => !ImageBuffer.isLoadingBuffer);

            activeProjectPath = ProjectLoader.PROJECTS_DIR_PATH + "/" + name;
            if (!activeProject.firstSceneName.Equals("") && activeProject.firstSceneName != null)
            {
                saveController.loadScene(activeProject.firstSceneName);
            }
            else
            {
                saveController.loadScene();
            }

            if (authoringMode)
            {
                uiController.setProjectMenuVisibility(false);
                if (complexEditing)
                {
                    uiController.toggleSceneEditUI(true, true);
                    uiController.toggleSceneJumperCreateFunctions(true);
                }
                else
                {
                    uiController.toggleSceneEditUI(true, false);
                    uiController.toggleSceneJumperCreateFunctions(false);
                }
                
            }
            else
            {
                uiController.setProjectMenuVisibility(false);
                uiController.toggleSceneEditUI(false);
                editModeController.toggleEditUI(false);
            }

            toggleMenuButtons(true);
            
            yield return null;
        }

        private void toggleMenuButtons(bool value)
        {
            loadProjectButton.interactable = value;
            newProjectButton.interactable = value;
            delProjectButton.interactable = value;
        }
        
        internal void deleteProject(string name)
        {
            projectLoader.deleteProject(name, true);
            OnProjectsChanged.Invoke();
        }

        public void UserChanged()
        {
            projectLoader.InitializeDirectories();
            OnProjectsChanged.Invoke();
        }

        public void changeInitialSceneToCurrent(bool setToCurrent)
        {
            if (setToCurrent)
            {
                activeProject.firstSceneName = activeScene.sceneName;
                projectLoader.updateProjectConfig(activeProject);
            }
            else
            {
                activeProject.firstSceneName = "";
                projectLoader.updateProjectConfig(activeProject);
            }
        }

        public void setNewSaveTimeStamp()
        {
            activeProject.lastLocalSave = TimeUtils.GetUnixTimestamp();
            projectLoader.updateProjectConfig(activeProject);
        }

        /// <summary>
        /// call that only right before uploading for a correct timestamp on server
        /// </summary>
        public bool setLastSaveToCurrentTime(string projectName)
        {
            ProjectData projectData = projectLoader.loadProject(projectName);
            projectData.lastRemoteSave = TimeUtils.GetUnixTimestamp();
            //TODO: May cause issues due to async file saving?
            projectLoader.updateProjectConfig(projectData);
            return true;
        }

        internal void uploadProject(string projectName)
        {
            remoteUpdater.remoteUploadProject(projectName);
        }

        internal IEnumerator updateRemoteProjectList()
        {
            if (PlayerPrefs.GetInt("loggedIn") != 0)
            {
                yield return remoteUpdater.GetRemoteProjectList();
                yield return true;
            }
            else
            {
                Debug.Log("not logged in, using local user");
                yield return false;
            }
        }

        public void pullRemoteProject(string projectName)
        {
            if (PlayerPrefs.GetInt("loggedIn") != 0)
            {
                remoteUpdater.pullRemoteProject(projectName);
            }
            else
            {
                Debug.Log("not logged in to download project!");
            }
        }
    }
}