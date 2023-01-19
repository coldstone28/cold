using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

namespace UnityEngine.UI.SaveSystem
{
    public class ProjectLoader : MonoBehaviour
    {
        private static string PROJECTS_SUBDIR = "Projects";
        private static string VIDEOPOOL_SUBDIR = "Videopool";
        private static string IMAGEPOOL_SUBDIR = "Imagepool";
        private static string AUDIOPOOL_SUBDIR = "Audiopool";
        
        private static string USER_DIR;
        
        public static string PROJECTS_DIR_PATH;
        public static string VIDEOPOOL_DIR_PATH;
        public static string IMAGEPOOL_DIR_PATH;
        public static string AUDIOPOOL_DIR_PATH;
        
        public static string PROJECT_CONFIG_FILE = "project_config.json";

        private void Awake()
        {
            InitializeDirectories();
        }

        //TODO: unterstand why that works, although it's in a static context
        internal void InitializeDirectories()
        {
            if (PlayerPrefs.GetInt("loggedIn") != 0)
            {
                USER_DIR = Application.persistentDataPath + "/users/" + PlayerPrefs.GetString("userid") + "/";
            }
            else
            {
                USER_DIR = Application.persistentDataPath + "/users/localUser/";
            }

            PROJECTS_DIR_PATH = USER_DIR + PROJECTS_SUBDIR;
            VIDEOPOOL_DIR_PATH = USER_DIR + VIDEOPOOL_SUBDIR;
            IMAGEPOOL_DIR_PATH = USER_DIR + IMAGEPOOL_SUBDIR;
            AUDIOPOOL_DIR_PATH = USER_DIR + AUDIOPOOL_SUBDIR;
            
            //does not override existing ones
            Directory.CreateDirectory(PROJECTS_DIR_PATH);
            Directory.CreateDirectory(VIDEOPOOL_DIR_PATH);
            Directory.CreateDirectory(IMAGEPOOL_DIR_PATH);
            Directory.CreateDirectory(AUDIOPOOL_DIR_PATH);
        }

        internal static string[] getProjectList()
        {
            return Directory.GetDirectories(PROJECTS_DIR_PATH)
                .Select(d => new DirectoryInfo(d).Name).ToArray();
        }

        /// <summary>
        /// Creates a new project with given name in projects directory.
        /// Returns data of this new created project.
        /// </summary>
        internal ProjectData createNewProject(string projectName = "Neues Projekt")
        {
            while (Directory.Exists(PROJECTS_DIR_PATH + "/" + projectName))
            {
                projectName += "_1"; //TODO: make this a counter
            }
            string newPath = PROJECTS_DIR_PATH + "/" + projectName;
            
            Directory.CreateDirectory(newPath);
            ProjectData projectData = new ProjectData(projectName);
            projectData.lastRemoteSave = 0;
            SaveIntoJson(projectData);
            using (File.Create(PROJECTS_DIR_PATH + "/" + projectName + "/" + ProjectController.SCENE_LIST_FILE))
            {
                
            }
            return projectData;
        }

        internal void updateProjectConfig(ProjectData projectData)
        {
            SaveIntoJson(projectData);
        }

        internal ProjectData loadProject(string projectName)
        {
            ProjectData projectData = LoadFromJson(projectName);
            return projectData;
        }

        internal void deleteProject(string name, bool deleteUnusedMedia = true)
        {
            string compDirName = "root: "+name;
            try
            {
                DirectoryInfo di = new DirectoryInfo(PROJECTS_DIR_PATH + "/" + name);
                
                if (deleteUnusedMedia)
                {
                    List<string>[] mediaLists = getMediaReferences(name);
                    List<string> videoFilesToDelete = mediaLists[0];
                    List<string> imagesToDelete = mediaLists[1];
                    List<string> audioFilesToDelete = mediaLists[2];

                    List<string>[] compMediaLists;
                
                    foreach (var dir in Directory.GetDirectories(PROJECTS_DIR_PATH))
                    {
                        compDirName = new DirectoryInfo(dir).Name;
                        if (!compDirName.Equals(name))
                        {
                            compMediaLists = getMediaReferences(compDirName);
                            videoFilesToDelete = videoFilesToDelete.Except(compMediaLists[0]).ToList();
                            imagesToDelete = imagesToDelete.Except(compMediaLists[1]).ToList();
                            audioFilesToDelete = imagesToDelete.Except(compMediaLists[2]).ToList();
                        }
                    }
                    
                    foreach (var videoFile in videoFilesToDelete)
                    {
                        File.Delete(VIDEOPOOL_DIR_PATH + "/" + videoFile);
                    }
                    foreach (var imageFile in imagesToDelete)
                    {
                        File.Delete(IMAGEPOOL_DIR_PATH + "/" + imageFile);
                    }
                    foreach (var audioFile in audioFilesToDelete)
                    {
                        File.Delete(AUDIOPOOL_DIR_PATH + "/" + audioFile);
                    }
                }
                
                di.Delete(true);
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.LogWarning(e);
            }
        }

        /// <summary>returns a 3-length array with all used videos on [0], images on [1] and audio on [2]
        /// in a given project</summary>
        private List<string>[] getMediaReferences(string projectName)
        {
            List<string>[] mediaLists = new List<string>[3];
            SceneData scene;
            List<string> videoFiles = new List<string>();
            List<string> images = new List<string>();
            List<string> audioFiles = new List<string>();
            foreach (var file in Directory.EnumerateFiles(PROJECTS_DIR_PATH + "/" + projectName, "*Data.json"))
            {
                string sceneString = File.ReadAllText(file);
                scene = JsonUtility.FromJson<SceneData>(sceneString);
                if (scene.videoFileName.EndsWith(".mp4") && !videoFiles.Contains(scene.videoFileName))
                {
                    videoFiles.Add(scene.videoFileName);
                }

                foreach (var interactable in scene.interactables)
                {
                    foreach (var target in interactable.targetScenes)
                    {
                        if (target.label.EndsWith(".png") || target.label.EndsWith(".jpg"))
                        {
                            if (!images.Contains(target.label))
                            {
                                images.Add(target.label);
                            }
                        }
                        if (target.label.EndsWith(".mp3") || target.label.EndsWith(".wav"))
                        {
                            if (!audioFiles.Contains(target.label))
                            {
                                audioFiles.Add(target.label);
                            }
                        }
                    }
                }
            }

            mediaLists[0] = videoFiles;
            mediaLists[1] = images;
            mediaLists[2] = audioFiles;
            return mediaLists;
        }
        
        private void SaveIntoJson(ProjectData projectData){
            string projectString = JsonUtility.ToJson(projectData);
            File.WriteAllText(PROJECTS_DIR_PATH + "/" + projectData.projectName + "/" + PROJECT_CONFIG_FILE, projectString);
        }

        private ProjectData LoadFromJson(string projectName)
        {
            ProjectData project = null;
        
            if(Directory.Exists(PROJECTS_DIR_PATH + "/" + projectName))
            {
                try
                {
                    string projectString = File.ReadAllText(PROJECTS_DIR_PATH + "/" + projectName + "/" + PROJECT_CONFIG_FILE);
                    project = JsonUtility.FromJson<ProjectData>(projectString);
                }
                catch (FileNotFoundException)
                {
                    Debug.LogError("Error while loading project: Directory exists, but there's no project_config.json");
                }
            }
            else
            {
                Debug.LogError("Error while loading project: No project folder called '" + projectName + "'");
            }
            return project;
        }
    }
}