using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.UI.SaveSystem
{
    public class SceneScanner : MonoBehaviour
    {
        public Text sceneNameInput;
        public Text videoFileNameInput;
        public Toggle isVideoLooping;
        public Transform interactionSphere;
        public Transform XRRig;

        internal SceneData scanCurrentScene(SceneData activeScene)
        {
            SceneData sceneData = activeScene;
            sceneData.sceneName = sceneNameInput.text;
            sceneData.videoFileName = videoFileNameInput.text;
            if (isVideoLooping.isOn)
            {
                sceneData.videoIsLooping = 1;
            } else {
                sceneData.videoIsLooping = 0;
            }
            sceneData.interactionSphereSize = interactionSphere.localScale.x;  //x/y/z is equal
            sceneData.xrRigRotation = XRRig.rotation.eulerAngles.y;
            Base360Interactable[] base360Interactables = interactionSphere.GetComponentsInChildren<Base360Interactable>();
            sceneData.interactables = new List<Interactable>();
            foreach (var bInteractable in base360Interactables)
            {
                Interactable interactable = new Interactable();
                interactable.id = bInteractable.id;
                interactable.type = bInteractable.type;
                interactable.questionOnFailed = bInteractable.questionOnFail;
                interactable.showOptionsOnEnable = bInteractable.showOptionsOnEnable ? 1 : 0;
                Transform wpTransform = bInteractable.gameObject.transform;
                var position = wpTransform.localPosition;
                interactable.locX = position.x;
                interactable.locY = position.y;
                interactable.locZ = position.z;
                var rotation = wpTransform.rotation;
                interactable.rotX = rotation.eulerAngles.x;
                interactable.rotY = rotation.eulerAngles.y;
                interactable.rotZ = rotation.eulerAngles.z;
                var localScale = wpTransform.localScale;
                interactable.scaX = localScale.x;
                interactable.scaY = localScale.y;
                interactable.scaZ = localScale.z;
                interactable.chartPosX = bInteractable.chartPosX;
                interactable.chartPosY = bInteractable.chartPosY;
                interactable.targetScenes = bInteractable.targetScenes;
                sceneData.interactables.Add(interactable);
            }
            return sceneData;
        }

        internal string getCurrentSceneName()
        {
            return sceneNameInput.text;
        }

        private Interactable getInteractableWithId(string id, SceneData sceneData)
        {
            foreach (var interactable in sceneData.interactables)
            {
                if (interactable.id.Equals(id))
                {
                    return interactable;
                }
            }
            return null;
        }
    }
}