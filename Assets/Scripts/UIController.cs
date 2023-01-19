using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using WebService;

namespace UnityEngine.UI.SaveSystem
{
    public class UIController : MonoBehaviour
    {
        public GameObject ProjectMenu;
        public ProjectList ProjectList;
        public GameObject UserMenu;
        public ServerConfigurator ServerConfigMenu;
        public GameObject ErrorPanel;
        public Text ErrorMsg;
        
        public GameObject sceneEditUI;
        //Panels that are hidden when in web supported mode
        public GameObject ObjectSpawnerCanvas;
        public GameObject TriggerBoardCanvas;
        public GameObject StartSceneButton;
        
        public GameObject EditModeSelectionPanel;

        public SceneList SceneJumper;
        
        public InputFieldPanel inputFieldPanel;
        public VerifyPanel VerifyPanel;
        
        public HintPanel HintPanel;
        
        public InputField inputField;
        public GameObject virtualKeyboard;
        public InteractionSphere360 interactionSphere;
        public GameObject warningFold;
        public GameObject successFold;

        //download information fields
        public Text labelProcessType;
        public Text labelProjectName;
        public Text labelType;
        public Text labelAmount;
        public GameObject ActiveDownloadPanel;
        
        public ProjectList projectList;
        
        private ProjectController projectController;

        private void Start()
        {
            projectController = FindObjectOfType<ProjectController>();
            if (!projectController)
            {
                Debug.LogError("Error in uiController: no projectController found in scene");
            }
        }

        public void setProjectMenuVisibility(bool toggle)
        {
            ProjectMenu.SetActive(toggle);
            UserMenu.SetActive(toggle);
        }

        public void toggleProjectMenu()
        {
            ProjectMenu.SetActive(!ProjectMenu.activeSelf);
            UserMenu.SetActive(!UserMenu.activeSelf);
        }
        
        public void toggleServerConfigMenu()
        {
            if (PlayerPrefs.HasKey("serverRoot"))
            {
                ServerConfigMenu.InputField.text = PlayerPrefs.GetString("serverRoot");
            }

            ServerConfigMenu.gameObject.SetActive(!ServerConfigMenu.gameObject.activeSelf);
        }

        public void showError(string msg)
        {
            ErrorPanel.SetActive(true);
            ErrorMsg.text = msg;
        }

        /// <summary>
        /// Toggles Scene Edit UI for creating/editing projects
        /// </summary>
        /// <param name="toggle">show/hide</param>
        /// <param name="complexMode">in complex mode show the trigger board to fully create scenes. Otherwise
        /// use WebInterface for that and just move existing triggers</param>
        public void toggleSceneEditUI(bool toggle, bool complexMode = false)
        {
            if (projectController.authoringMode)
            {
                sceneEditUI.SetActive(toggle);
                if (!complexMode)
                {
                    ObjectSpawnerCanvas.SetActive(false);
                    TriggerBoardCanvas.SetActive(false);
                    StartSceneButton.SetActive(false);
                }
                else
                {
                    ObjectSpawnerCanvas.SetActive(true);
                    TriggerBoardCanvas.SetActive(true);
                    StartSceneButton.SetActive(true);
                }
            }
            else
            {
                sceneEditUI.SetActive(false);
            }
        }

        /// <summary>
        /// show and hide "create new scene" buttons in sceneList of SceneJumper, like neccessary in complex editing mode.
        /// </summary>
        /// <param name="toggle">hide/show</param>
        public void toggleSceneJumperCreateFunctions(bool toggle)
        {
            SceneJumper.IsComplexEditing = toggle;
            SceneJumper.reloadScenes();
        }
        
        public void toggleDownloadStatePanel(bool toggle)
        {
            ActiveDownloadPanel.SetActive(toggle);
        }
        
        public void toggleEditModeSelectionPanel(bool toggle)
        {
            EditModeSelectionPanel.SetActive(toggle);
        }
        
        internal void hideInteractionSphere()
        {
            //doing it like this keeps neccessary events from interactables enabled but makes them invisible 
            interactionSphere.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }

        internal void showInteractionSphere()
        {
            interactionSphere.gameObject.transform.localScale = Vector3.one * interactionSphere.currentSphereSize;
        }

        internal void showWarningFold()
        {
            warningFold.gameObject.SetActive(true);
        }

        internal void hideWarningFold()
        {
            warningFold.gameObject.SetActive(false);
        }
        
        internal void showSuccessFold()
        {
            successFold.gameObject.SetActive(true);
        }

        internal void hideSuccessFold()
        {
            successFold.gameObject.SetActive(false);
        }

        internal void openHintPanel(string hint)
        {
            HintPanel.setHint(hint);
            HintPanel.gameObject.SetActive(true);
        }

        /// <summary>
        /// Opens an Input that calls a given method [like createNewProject(string name)] on submit and passes input text
        /// Will be hidden automatically after submitting
        /// </summary>
        internal void openGenericInputField(UnityAction<string> methodToCallOnSubmit, bool isInputForQuestionText = false)
        {
            virtualKeyboard.SetActive(true);
            if (isInputForQuestionText)
            {
                inputFieldPanel.prepareForSetQuestion();
            }
            else
            {
                inputFieldPanel.prepareForSetGeneric();
            }
            inputField.text = "";
            inputFieldPanel.gameObject.SetActive(true);
            inputFieldPanel.OnSubmitInput.RemoveAllListeners();
            inputFieldPanel.OnSubmitInput.AddListener(methodToCallOnSubmit);
        }


        /// <summary>
        /// Opens a Yes/No Input that shows a questions and calls a given method [like createNewProject(string name)] on clicking "Yes"
        /// If theres a control sigh for noIsTrue, then clicking on "No" will submit with true instead of clicking "Yes"
        /// </summary>
        internal void openGenericVerifyField(UnityAction<bool> methodToCallOnSubmit, string question)
        {
            //TODO: forbid $ sign as a char in Input or find better control sign
            bool noIsTrue = question[0].Equals('t') && question[1].Equals('$');

            VerifyPanel.gameObject.SetActive(true);
            if (question[1].Equals('$'))
            {
                VerifyPanel.question.text = question.Substring(2); //delete control sign from question text
            }
            else
            {
                VerifyPanel.question.text = question; 
            }

            if (noIsTrue)
            {
                VerifyPanel.setNoAsSubmit(); //if there's no control sign then take "yes" as submit true
            }

            VerifyPanel.OnSubmit.RemoveAllListeners();
            VerifyPanel.OnSubmit.AddListener(methodToCallOnSubmit);
        }
        
        private void OnDisable()
        {
            inputFieldPanel.OnSubmitInput.RemoveAllListeners();
        }
    }
}