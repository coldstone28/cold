using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine.Events;

namespace UnityEngine.UI.SaveSystem
{
    public class SceneLoader : MonoBehaviour
    {
        public Text sceneNameInput;
        public Text videoFileNameInput;
        public Toggle isVideoLooping;
        public GameObject interactionSphere;
        public InteractionSphere360 interactionSphereComponent;
        public Transform XRRig;
        private ObjectSpawner objectSpawner;
        private VideoController videoController;
        internal ProjectController projectController;
        public Slider interactSphereSizeSlider;
        public Slider xrRigRotationSlider;
        public Toggle initialSceneToggle;
        private UnityAction loadDelayed = new UnityAction(delegate {  });
        
        internal bool isUpdatingFile = false;
        
        void Start()
        {
            objectSpawner = FindObjectOfType<ObjectSpawner>();
            if (!objectSpawner)
            {
                Debug.LogError("Error in SceneLoader: Couldn't find objectSpawner in scene");
            }
            videoController = FindObjectOfType<VideoController>();
            if (!videoController)
            {
                Debug.LogError("Error in SceneLoader: Couldn't find videoController in scene");
            }
            projectController = FindObjectOfType<ProjectController>();
            if (!projectController)
            {
                Debug.LogError("Error in SceneLoader: Couldn't find projectController in scene");
            }
        }
        
        internal void LoadScene(SceneData sceneData, bool isInitialScene = false)
        {
            videoController.onVideoPlaybackStarted.RemoveListener(loadDelayed);

            //destroy all Base360Interactables
            foreach (Transform child in interactionSphere.transform) {
                Destroy(child.gameObject);
            }
            objectSpawner.clearSpawner();
            
            setSceneName(sceneData.sceneName);
            videoFileNameInput.text = sceneData.videoFileName;
            
            /* interaction sphere setup */
            interactionSphere.transform.localScale = Vector3.one * sceneData.interactionSphereSize;
            interactionSphereComponent.currentSphereSize = sceneData.interactionSphereSize;
            interactSphereSizeSlider.value = sceneData.interactionSphereSize;

            /* video looping setup */
            if (sceneData.videoIsLooping == 0)
            {
                isVideoLooping.isOn = false;
                videoController.setIsVideoLooping(false);
            } else {
                isVideoLooping.isOn = true;
                videoController.setIsVideoLooping(true);
            }

            loadDelayed = delegate { loadObjectsDelayed(sceneData, isInitialScene); }; //after first frame
            
            /* load interactables after video started */
            videoController.onVideoPlaybackStarted.AddListener(loadDelayed);
            if (sceneData.videoFileName != null && !sceneData.videoFileName.Equals(""))
            {
                videoController.PlaySceneVideo(sceneData.videoFileName);
            }
            else
            {
                videoController.StopCurrentVideo();
            }
        }

        private void loadObjectsDelayed(SceneData sceneData, bool isInitialScene = false)
        {
            videoController.onVideoPlaybackStarted.RemoveListener(loadDelayed);
            /* xr rig rotation setup delayed as well */
            xrRigRotationSlider.value = sceneData.xrRigRotation;
            XRRig.rotation = Quaternion.AngleAxis(sceneData.xrRigRotation, Vector3.up);
            
            foreach (var interactable in sceneData.interactables)
            {
                spawnInteractable(interactable);
                //TODO: Too much resources needed so far, and needs null check btw
                
                /*foreach (var targetScene in interactable.targetScenes)
                {
                    string targetSceneVideo = projectController.saveController.getScene(targetScene.targetSceneName)
                        .videoFileName;
                    videoController.AddToVideoBuffer(targetSceneVideo);
                }*/
            }
            if (isInitialScene)
            {
                initialSceneToggle.isOn = true;
            }
        }

        internal void setSceneName(string sceneName)
        {
            sceneNameInput.text = sceneName;
        }

        public void spawnInteractable(Interactable interactable)
        {
            GameObject triggerToSpawn;
            if (interactable.type == 0)
            {
                triggerToSpawn = objectSpawner.TriggerDoor;
            }
            else if (interactable.type == 1)
            {
                triggerToSpawn = objectSpawner.TriggerTalk;
            }
            else if (interactable.type == 2)
            {
                triggerToSpawn = objectSpawner.TriggerRndWaypoint;
            }
            else if (interactable.type == 3)
            {
                triggerToSpawn = objectSpawner.TriggerAction;
            }
            else if (interactable.type == 4)
            {
                triggerToSpawn = objectSpawner.TriggerPlaythrough;
            }
            else if (interactable.type == 5)
            {
                triggerToSpawn = objectSpawner.TriggerInvestigate;
            }
            else if (interactable.type == 6)
            {
                triggerToSpawn = objectSpawner.TriggerUse;
            }
            else if (interactable.type == 7)
            {
                triggerToSpawn = objectSpawner.TriggerImage;
            }
            else if (interactable.type == 8)
            {
                triggerToSpawn = objectSpawner.TriggerImageUse;
            }
            else if (interactable.type == 9)
            {
                triggerToSpawn = objectSpawner.TriggerHint;
            }
            else if (interactable.type == 10)
            {
                triggerToSpawn = objectSpawner.TriggerAudio;
            }
            else if (interactable.type == 11)
            {
                triggerToSpawn = objectSpawner.TriggerAudioInteractable;
            }
            else if (interactable.type == 12)
            {
                triggerToSpawn = objectSpawner.TriggerAudioWaypoint;
            }
            else if (interactable.type == 13)
            {
                triggerToSpawn = objectSpawner.TriggerChecklistRoot;
            }
            else if (interactable.type == 14)
            {
                triggerToSpawn = objectSpawner.TriggerCheckbox;
            }
            else
            {
                triggerToSpawn = objectSpawner.TriggerCube;
            }
            
            GameObject spawnedObject = Instantiate(triggerToSpawn, new Vector3(), Quaternion.identity);
            Base360Interactable spawnedInteractable = spawnedObject.GetComponent<Base360Interactable>();
            if ((interactable.type == 3 || interactable.type == 4 || interactable.type == 9 || interactable.type == 10) && !projectController.authoringMode)
            {
                spawnedInteractable.hideInteractable();
            }
            spawnedObject.transform.parent = interactionSphere.transform;
            spawnedObject.transform.localPosition = new Vector3(interactable.locX, interactable.locY, interactable.locZ);
            spawnedInteractable.showOptionsOnEnable =
                interactable.showOptionsOnEnable != 0;
            spawnedObject.transform.rotation =
                Quaternion.Euler(interactable.rotX, interactable.rotY, interactable.rotZ);
            spawnedObject.transform.localScale = new Vector3(interactable.scaX, interactable.scaY, interactable.scaZ);
            spawnedInteractable.InteractionSphere = interactionSphere;
            spawnedInteractable.id = interactable.id;
            spawnedInteractable.chartPosX = interactable.chartPosX;
            spawnedInteractable.chartPosY = interactable.chartPosY;
            Canvas[] canvas = spawnedObject.GetComponentsInChildren<Canvas>(true);
            foreach (var can in canvas)
            {
                can.worldCamera = Camera.main;
            }
            spawnedInteractable.targetScenes = interactable.targetScenes;
            spawnedInteractable.setQuestionOnFail(interactable.questionOnFailed);
            spawnedInteractable.updateGUI();
        }

        internal void addToSceneList(string sceneName)
        {
            isUpdatingFile = true;
            StartCoroutine(addToSceneListCouroutine(sceneName));
        }

        IEnumerator addToSceneListCouroutine(string sceneName)
        {
            yield return new WaitUntil(() => File.Exists(projectController.activeProjectPath + "/" + ProjectController.SCENE_LIST_FILE));
            
            using (StreamWriter sw =
                File.AppendText(projectController.activeProjectPath + "/" + ProjectController.SCENE_LIST_FILE))
            {
                sw.WriteLine(sceneName);
                sw.Close();
                sw.Dispose();
                isUpdatingFile = false;
            }
        }
        
        private void setIsUpdatingFileFalse(object sender, EventArgs e)
        {
            isUpdatingFile = false;
        }
        
        internal void renameSceneListFile(string oldName, string newName)
        {
            string[] scenesInList = getSceneList();
            for (var index = 0; index < scenesInList.Length; index++)
            {
                if (scenesInList[index].Equals(oldName))
                {
                    scenesInList[index] = newName;
                }
            }

            using (StreamWriter sw =
                File.CreateText(projectController.activeProjectPath + "/" + ProjectController.SCENE_LIST_FILE))
            {
                foreach (string scene in scenesInList)
                {
                    sw.WriteLine(scene);
                }
            }
        }
        
        internal string[] getSceneList()
        {
            List<string> lineList = new List<string>();
            try
            {
                using (var sr = new StreamReader(projectController.activeProjectPath + "/" + ProjectController.SCENE_LIST_FILE))
                {
                    while (sr.Peek() >= 0)
                    {
                        lineList.Add(sr.ReadLine());
                    }
                    return lineList.ToArray();
                }
            }
            catch (IOException e)
            {
                Debug.Log("The" + ProjectController.SCENE_LIST_FILE + "could not be read:");
                Debug.Log(e.Message);
            }

            return new string[0];
        }
    }
}