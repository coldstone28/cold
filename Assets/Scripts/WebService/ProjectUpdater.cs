using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using LightBuzz.Archiver;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI.WebService;
using WebService;

namespace UnityEngine.UI.SaveSystem
{
    public class ProjectUpdater : MonoBehaviour
    {
        private ProjectController projectController;

        private string connectionErrorMsg =
            "Keine Internetverbindung möglich. Projekte können nicht aktualisiert werden.";
        
        private void Start()
        {
            projectController = FindObjectOfType<ProjectController>();
            if (!projectController)
            {
                Debug.LogError("no project controller found in scene");
            }
        }

        internal void remoteUploadProject(string projectName)
        {
            StartCoroutine(UploadProject(projectName));
        }

        internal void pullRemoteProject(string projectName)
        {
            StartCoroutine(DownloadRemoteProject(projectName));
        }
        
        private IEnumerator UploadProject(string projectName)
        {
            projectController.uiController.toggleDownloadStatePanel(true);
            projectController.uiController.labelProjectName.text = projectName.Replace("\n", "");
            projectController.uiController.labelProcessType.text = "Upload";
            
            projectController.uiController.labelType.text = "Sende Projektdateien";
            projectController.uiController.labelAmount.text = "";
            
            string projectPath = ProjectLoader.PROJECTS_DIR_PATH+"/"+projectName;

            yield return projectController.setLastSaveToCurrentTime(projectName);

            if (File.Exists(ProjectLoader.PROJECTS_DIR_PATH + "/" + projectName + ".zip"))
            {
                File.Delete(ProjectLoader.PROJECTS_DIR_PATH + "/" + projectName + ".zip");
            }
            ZipFile.CreateFromDirectory(projectPath, ProjectLoader.PROJECTS_DIR_PATH + "/" + projectName + ".zip");

            yield return new WaitUntil(() => File.Exists(ProjectLoader.PROJECTS_DIR_PATH + "/" + projectName + ".zip"));
            
            FileInfo info = new FileInfo(ProjectLoader.PROJECTS_DIR_PATH + "/" + projectName + ".zip");
            
            Debug.Log("File has " + info.Length + " byte");
            
            projectPath = ProjectLoader.PROJECTS_DIR_PATH+"/"+projectName + ".zip";
            
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormFileSection("file", File.ReadAllBytes(projectPath), Path.GetFileName(projectPath), "multipart/form-data"));
            formData.Add(new MultipartFormDataSection("userid", PlayerPrefs.GetString("userid")));
            
            UnityWebRequest req = UnityWebRequest.Post(PlayerPrefs.GetString("serverRoot") + "api/projectuploader.php", formData);
            
            yield return req.SendWebRequest();

            if (req.isHttpError || req.isNetworkError)
            {
                Debug.Log(req.error);
                projectController.uiController.showError(connectionErrorMsg);
            }
            else
            {
                Debug.Log("Uploaded file successfully");
                deleteTempProjectArchives();
            }
            projectController.uiController.toggleDownloadStatePanel(false);
            projectController.OnProjectsChanged.Invoke();
        }

        internal IEnumerator GetRemoteProjectList()
        {
            string userid = PlayerPrefs.GetString("userid");
            string url = PlayerPrefs.GetString("serverRoot") + "api/readprojectdir.php?userid="+userid;
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.Send();
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    Debug.Log("projectList not found on server url: " + url);
                    projectController.uiController.showError(connectionErrorMsg);
                }
                else
                {
                    RemoteProjectList projectList = JsonUtility.FromJson<RemoteProjectList>(www.downloadHandler.text);
                    string filePath = ProjectLoader.PROJECTS_DIR_PATH + "/" + ProjectController.REMOTE_PROJECT_LIST_FILE;
                    
                    if (!File.Exists(filePath))
                    {
                        using (File.Create(filePath))
                        {
                        }
                    }

                    using (var stream = new FileStream(filePath, FileMode.Truncate))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            if (projectList != null)
                            {
                                writer.WriteLine(www.downloadHandler.text);
                            }
                        }
                    }
                }
            }
        }
        
        private IEnumerator DownloadRemoteProject(string projectName)
        {
            string fileName = projectName.Replace("\n", "") + ".zip";
            string userid = PlayerPrefs.GetString("userid");
            string rooturl = PlayerPrefs.GetString("serverRoot") + "userdata/" + userid + "/";
            string projectsURL;
            string savePath;
            
            if (fileName.Any())
            {
                projectController.uiController.toggleDownloadStatePanel(true);
                projectController.uiController.labelProjectName.text = projectName.Replace("\n", "");
                projectController.uiController.labelProcessType.text = "Download";
                
                //download project json data
                savePath = string.Format("{0}/{1}", ProjectLoader.PROJECTS_DIR_PATH, fileName);

                projectsURL = rooturl + "projects/" + fileName;
                
                yield return StartCoroutine(DownloadFile(projectsURL, savePath, true));

                if (File.Exists(savePath))
                {
                    string newFolder = fileName.Replace(".zip", "");

                    if (Directory.Exists(ProjectLoader.PROJECTS_DIR_PATH + "/" + newFolder))
                    {
                        Directory.Delete(ProjectLoader.PROJECTS_DIR_PATH + "/" + newFolder, true);
                    }

                    Directory.CreateDirectory(ProjectLoader.PROJECTS_DIR_PATH + "/" + newFolder);

                    projectController.uiController.labelType.text = "Lade Szenen";
                    projectController.uiController.labelAmount.text = "";

                    ZipFile.ExtractToDirectory(savePath, ProjectLoader.PROJECTS_DIR_PATH + "/" + newFolder);

                    //download neccessary media files
                    SceneData scene;
                    List<string> videoFiles = new List<string>();
                    List<string> images = new List<string>();
                    List<string> audioFiles = new List<string>();
                    foreach (var file in Directory.EnumerateFiles(ProjectLoader.PROJECTS_DIR_PATH + "/" + newFolder,
                        "*Data.json"))
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
                                else if (target.label.EndsWith(".mp3") || target.label.EndsWith(".wav"))
                                {
                                    if (!audioFiles.Contains(target.label))
                                    {
                                        audioFiles.Add(target.label);
                                    }
                                }
                            }
                        }
                    }

                    Directory.CreateDirectory(ProjectLoader.VIDEOPOOL_DIR_PATH);
                    Directory.CreateDirectory(ProjectLoader.IMAGEPOOL_DIR_PATH);
                    Directory.CreateDirectory(ProjectLoader.AUDIOPOOL_DIR_PATH);

                    projectController.uiController.labelType.text = "Lade Bilder";
                    int counter = 0;
                    foreach (var imageFile in images)
                    {
                        counter++;
                        projectController.uiController.labelAmount.text = counter + "/" + images.Count;
                        string imagesURL = rooturl + "images/" + imageFile;
                        savePath = string.Format("{0}/{1}", ProjectLoader.IMAGEPOOL_DIR_PATH, imageFile);
                        yield return StartCoroutine(DownloadFile(imagesURL, savePath, false));
                    }

                    counter = 0;
                    projectController.uiController.labelType.text = "Lade Audiodateien";
                    foreach (var audioFile in audioFiles)
                    {
                        counter++;
                        projectController.uiController.labelAmount.text = counter + "/" + audioFiles.Count;
                        string audioURL = rooturl + "audio/" + audioFile;
                        savePath = string.Format("{0}/{1}", ProjectLoader.AUDIOPOOL_DIR_PATH, audioFile);
                        yield return StartCoroutine(DownloadFile(audioURL, savePath, false));
                    }

                    counter = 0;
                    projectController.uiController.labelType.text = "Lade Videos";
                    foreach (var videoFile in videoFiles)
                    {
                        counter++;
                        projectController.uiController.labelAmount.text = counter + "/" + videoFiles.Count;
                        string videosURL = rooturl + "videos/" + videoFile;
                        savePath = string.Format("{0}/{1}", ProjectLoader.VIDEOPOOL_DIR_PATH, videoFile);
                        yield return StartCoroutine(DownloadFile(videosURL, savePath, false));
                    }
                }
            }
            projectController.OnProjectsChanged.Invoke();
            projectController.uiController.toggleDownloadStatePanel(false);
            deleteTempProjectArchives();
        }

        private IEnumerator DownloadFile(string url, string savePath, bool overwrite)
        {
            if (File.Exists(savePath) && !overwrite)
            {
                
            }
            else
            {
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                using (UnityWebRequest www = UnityWebRequest.Get(url))
                {
                    yield return www.Send();

                    if (www.isNetworkError || www.isHttpError)
                    {
                        Debug.Log(www.error);
                        Debug.Log("image not found on server url: " + url);
                        projectController.uiController.showError(connectionErrorMsg);
                    }
                    else
                    {
                        File.WriteAllBytes(savePath, www.downloadHandler.data);
                    }
                }
            }
        }

        private void deleteTempProjectArchives()
        {
            DirectoryInfo di = new DirectoryInfo(ProjectLoader.PROJECTS_DIR_PATH);
            FileInfo[] files = di.GetFiles("*.zip").Where(p => p.Extension == ".zip").ToArray();
            foreach (FileInfo file in files)
            {
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch (FileNotFoundException e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}