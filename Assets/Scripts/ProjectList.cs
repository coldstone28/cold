using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UI.SaveSystem;
using UnityEngine.UI.WebService;
using UnityEngine.Video;

enum VersionState
{
    NotCloned,
    UpToDate,
    BehindRemote,
    BeforeRemote,
    LocalOnly,
    ChangesLocalAndRemote
}

public class ProjectList : MonoBehaviour
{
    private static string SURE_TO_DELETE = "Sind Sie sicher, dass sie das Projekt löschen wollen?";
    
    public Button templateButton;
    public Button templateButtonPulled;
    public Button templateButtonPullAvailable;
    public Button templateButtonPullNeccessary;
    public Button templateButtonPushAvailable;
    public Button templateButtonLocalOnly;
    
    public Button openButton;
    public Button editButton;
    public Button newProjectButton;
    public Button downloadButton;
    public Button uploadButton;

    public Text btnDownloadLabel;

    public Text selProjectTitle;
    public Text selProjectSize;
    public Text selProjectSceneCount;
    
    private string selectedProject;
    
    public Image listGrid;

    private ProjectController projectController;

    private UIController UIController;

    private void Start()
    {
        openButton.interactable = false;
        //reloadProjects();
        projectController = FindObjectOfType<ProjectController>();
        if (!projectController)
        {
            Debug.LogError("Error in Project List: Couldn't find projectController in Scene");
        }
        projectController.OnProjectsChanged.AddListener(reloadProjects);
        
        UIController = FindObjectOfType<UIController>();
        if (!UIController)
        {
            Debug.LogError("Error in Project List: Couldn't find UIController in Scene");
        }
        
        openButton.interactable = false;
        uploadButton.interactable = false;
        downloadButton.interactable = false;
        editButton.interactable = false;
        reloadProjects();
    }

    public void reloadProjects()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(reloadProjectsAsync());
        }
    }

    private IEnumerator reloadProjectsAsync()
    {
        foreach (Transform child in listGrid.transform)
        {
            Destroy(child.gameObject);
        }

        //just locals
        List<ProjectData> localProjectConfigs = new List<ProjectData>();
        foreach (var dir in Directory.GetDirectories(ProjectLoader.PROJECTS_DIR_PATH))
        {
            try
            {
                ProjectData currentProjectData;
                string projectConfig = File.ReadAllText(dir + "/" + "project_config.json");
                localProjectConfigs.Add(JsonUtility.FromJson<ProjectData>(projectConfig));
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.Log(e.Message);
            }
        }

        //add remotes
        yield return projectController.updateRemoteProjectList();
        RemoteData[] remoteData = getRemoteProjectList();
        
        Button newEntry;
        
        foreach (var project in localProjectConfigs)
        {
            bool remoteFound = false;
            foreach (var remoteProject in remoteData)
            {
                if (project.projectName.Equals(remoteProject.projectName) && project.lastRemoteSave != null)
                {
                    remoteFound = true;
                    if (project.lastRemoteSave == remoteProject.lastRemoteSave &&
                        project.lastLocalSave > remoteProject.lastRemoteSave)
                    {
                        newEntry = Instantiate(templateButtonPushAvailable);
                        newEntry.onClick.AddListener(delegate
                        {
                            setSelectedProject(project.projectName, VersionState.BeforeRemote, remoteProject.sceneCount, remoteProject.projectSize);
                        });
                    }
                    else if (project.lastRemoteSave == remoteProject.lastRemoteSave && project.lastLocalSave <= remoteProject.lastRemoteSave)
                    {
                        newEntry = Instantiate(templateButtonPulled);
                        newEntry.onClick.AddListener(delegate
                        {
                            setSelectedProject(project.projectName, VersionState.UpToDate, remoteProject.sceneCount, remoteProject.projectSize);
                        });
                    }
                    else if (project.lastRemoteSave <= remoteProject.lastRemoteSave && 
                        project.lastLocalSave <= project.lastRemoteSave)
                    {
                        newEntry = Instantiate(templateButtonPullAvailable);
                        newEntry.onClick.AddListener(delegate
                        {
                            setSelectedProject(project.projectName, VersionState.BehindRemote, remoteProject.sceneCount, remoteProject.projectSize);
                        });
                    }
                    else if (project.lastRemoteSave <= remoteProject.lastRemoteSave && 
                        project.lastLocalSave > project.lastRemoteSave)
                    {
                        newEntry = Instantiate(templateButtonPullNeccessary);
                        newEntry.onClick.AddListener(delegate
                        {
                            setSelectedProject(project.projectName, VersionState.ChangesLocalAndRemote, remoteProject.sceneCount, remoteProject.projectSize);
                        });
                    }
                    else
                    {
                        newEntry = Instantiate(templateButtonPullNeccessary);
                        newEntry.onClick.AddListener(delegate
                        {
                            setSelectedProject(project.projectName, VersionState.ChangesLocalAndRemote, remoteProject.sceneCount, remoteProject.projectSize);
                        });
                    }
                    newEntry.GetComponentInChildren<Text>().text = project.projectName;
                    newEntry.transform.SetParent(listGrid.transform, false);
                }
            }

            if (!remoteFound)
            {
                newEntry = Instantiate(templateButtonLocalOnly);
                newEntry.onClick.AddListener(delegate
                {
                    setSelectedProject(project.projectName, VersionState.LocalOnly);
                });
                
                newEntry.GetComponentInChildren<Text>().text = project.projectName;
                newEntry.transform.SetParent(listGrid.transform, false);
            }
        }

        foreach (var remoteProject in remoteData)
        {
            bool localFound = false;
            foreach (var localProject in localProjectConfigs)
            {
                if (localProject.projectName.Equals(remoteProject.projectName))
                {
                    localFound = true;
                }
            }

            if (!localFound)
            {
                newEntry = Instantiate(templateButton);
                newEntry.onClick.AddListener(delegate
                {
                    setSelectedProject(remoteProject.projectName, VersionState.NotCloned, remoteProject.sceneCount, remoteProject.projectSize);
                });
                
                newEntry.GetComponentInChildren<Text>().text = remoteProject.projectName;
                newEntry.transform.SetParent(listGrid.transform, false);
            }
        }
    }

    private RemoteData[] getRemoteProjectList()
    {
        RemoteProjectList remoteProjectList = null;
        
        if(Directory.Exists(ProjectLoader.PROJECTS_DIR_PATH + "/"))
        {
            try
            {
                string remoteProjectListString = File.ReadAllText(ProjectLoader.PROJECTS_DIR_PATH + "/" + ProjectController.REMOTE_PROJECT_LIST_FILE);
                remoteProjectList = JsonUtility.FromJson<RemoteProjectList>(remoteProjectListString);
                return remoteProjectList.remoteData.ToArray();
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("The " + ProjectController.REMOTE_PROJECT_LIST_FILE + " could not be read:");
                Debug.Log(e.Message);
            }
        }
        else
        {
            Debug.LogError("Folder not found: " + ProjectLoader.PROJECTS_DIR_PATH);
        }

        return Array.Empty<RemoteData>();
    }
    
    void setSelectedProject(string name, VersionState versionState, int sceneCount = 0, int size = 0)
    {
        selectedProject = name;
        if (selectedProject.Any())
        {
            selProjectTitle.text = name;

            if (sceneCount > 0)
            {
                selProjectSceneCount.text = sceneCount.ToString();
            }
            else
            {
                selProjectSceneCount.text = "unbekannt";
            }

            if (size > 0 && size / 1000000 == 0)
            {
                selProjectSize.text = "<1mb";
            }
            else if (size > 0)
            {
                selProjectSize.text = size / 1000000 + "mb";
            }
            else
            {
                selProjectSize.text = "unbekannt";
            }

            if (versionState == VersionState.UpToDate || versionState == VersionState.BehindRemote ||
                versionState == VersionState.BeforeRemote || versionState == VersionState.LocalOnly)
            {
                openButton.interactable = true;
                editButton.interactable = true;
            }
            else
            {
                openButton.interactable = false;
                editButton.interactable = false;
            }

            if (versionState == VersionState.UpToDate || versionState == VersionState.BeforeRemote || versionState == VersionState.LocalOnly)
            {
                uploadButton.interactable = true;
                downloadButton.interactable = false;
            }
            else
            {
                uploadButton.interactable = false;
            }
            if (versionState == VersionState.BehindRemote || versionState == VersionState.ChangesLocalAndRemote || versionState == VersionState.NotCloned)
            {
                downloadButton.interactable = true;
            }
            else
            {
                downloadButton.interactable = false;
            }

            if (versionState == VersionState.BehindRemote)
            {
                btnDownloadLabel.text = "Szenario aktualisieren";
            }
            else
            {
                btnDownloadLabel.text = "Szenario herunterladen";
            }

            if (PlayerPrefs.GetInt("loggedIn") == 0)
            {
                downloadButton.interactable = false;
                uploadButton.interactable = false;
            }
        }
        else
        {
            openButton.interactable = false;
            uploadButton.interactable = false;
            downloadButton.interactable = false;
        }
    }

    public void openSelectedProject()
    {
        if (selectedProject.Any())
        {
            projectController.authoringMode = false;
            projectController.openProject(selectedProject);
        }
    }

    public void openEditModeSelection()
    {
        if (selectedProject.Any())
        {
            UIController.toggleEditModeSelectionPanel(true);
        }
    }
    
    public void editSelectedProject(bool complexMode)
    {
        UIController.toggleEditModeSelectionPanel(false);
        if (selectedProject.Any())
        {
            projectController.authoringMode = true;
            if (complexMode)
            {
                projectController.complexEditing = true;
            }
            else
            {
                projectController.complexEditing = false;
            }
            projectController.openProject(selectedProject);
        }
    }

    public void uploadSelectedProject()
    {
        if (selectedProject.Any())
        {
            projectController.uploadProject(selectedProject);
        }
    }

    public void downloadSelectedProject()
    {
        if (selectedProject.Any())
        {
            projectController.pullRemoteProject(selectedProject);
        }
    }

    public void deleteSelectedProject()
    {
        if (selectedProject.Any())
        {
            UIController.openGenericVerifyField(deleteSelectedProject, SURE_TO_DELETE);
        }
    }
    
    private void deleteSelectedProject(bool userIsSure)
    {
        if (userIsSure)
        {
            projectController.deleteProject(selectedProject);
        }
    }
    
    public void openNewProject()
    {
        projectController.authoringMode = true;
        projectController.complexEditing = true;
        UIController.openGenericInputField(projectController.openNewProject);
    }
}
