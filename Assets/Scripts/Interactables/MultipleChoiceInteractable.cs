using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.SaveSystem;

public class MultipleChoiceInteractable : Base360Interactable
{
    public GameObject SceneList;

    public MultipleChoiceOption[] mcOptions; //TODO: So far there are 4 hard implemented options in GUI, this should better be dynamic

    private MultipleChoiceOption currentlyEditedOption;

    public Toggle showOptionsOnEnabledToggle;

    internal override int type { get; set; }

    protected override void Awake()
    {
        for (var index = 0; index < mcOptions.Length; index++)
        {
            targetScenes.Add(new TargetScene("","", ""));
            type = 1;
            base.Awake();
        }
    }

    void Start()
    {
        if (!SceneList)
        {
            Debug.Log("no videoFileList object set in MultipleChoiceInteractable");
        }
        base.Start();
        editModeController.OnStateChanged.AddListener(hideOptions);
        if (showOptionsOnEnable)
        {
            OnClicked();
        }
    }

    private void hideOptions()
    {
        foreach (var mcOption in mcOptions)
        {
            mcOption.labelBtn.gameObject.SetActive(false);
            mcOption.targetBtn.gameObject.SetActive(false);
        }
        SceneList.SetActive(false);
    }
    
    public override void OnClicked()
    {
        foreach (var mcOption in mcOptions)
        {
            if (mcOption.labelBtn.gameObject.activeInHierarchy)
            {
                mcOption.labelBtn.gameObject.SetActive(false);
            }
            else
            {
                //TODO: this string check is based on the hard written text in the mcButton prefab -> that's to improve
                if (mcOption.labelBtnText.text.Any() && !mcOption.labelBtnText.text.Equals("angezeigter Text") || editModeController.isEditModeActive)
                {
                    mcOption.labelBtn.gameObject.SetActive(true);
                }
            }
        }
        if (editModeController.isEditModeActive)
        {
            if (SceneList.activeInHierarchy)
            {
                SceneList.SetActive(false);
                foreach (var mcOption in mcOptions)
                {
                    mcOption.targetBtn.gameObject.SetActive(false);
                }
            } 
            else
            {
                SceneList.SetActive(true);
                SceneList.GetComponent<SceneList>().reloadScenes();
                foreach (var mcOption in mcOptions)
                {
                    mcOption.targetBtn.gameObject.SetActive(true);
                }
            }
        }
    }

    internal override void updateGUI()
    {
        for (var index = 0; index < targetScenes.Count; index++)
        {
            mcOptions[index].labelBtnText.text = targetScenes[index].label;
            mcOptions[index].targetBtnText.text = targetScenes[index].targetSceneName;
            mcOptions[index].hiddenDataText.text = targetScenes[index].hiddenData;
        }
        showOptionsOnEnabledToggle.isOn = showOptionsOnEnable;
    }
    
    internal override void setTargetScene(string sceneName)
    {
        if (!sceneName.Equals("TargetSceneName") && currentlyEditedOption)
        {
            targetScenes[currentlyEditedOption.optionIndex].targetSceneName = sceneName;
            updateGUI();
        }
    }

    internal override void setTargetLabel(string text)
    {
        targetScenes[currentlyEditedOption.optionIndex].label = text;
        updateGUI();
    }
    
    internal override void setTargetHiddenData(string stringData)
    {
        targetScenes[currentlyEditedOption.optionIndex].hiddenData = stringData;
        updateGUI();
    }
    
    public void setTargetLabelByInput()
    {
        saveController.uiController.openGenericInputField(setTargetLabel);
    }

    public void OnLabelSelected(MultipleChoiceOption mcOption)
    {
        if (editModeController.isEditModeActive)
        {
            currentlyEditedOption = mcOption;
            setTargetLabelByInput();
        }
        else
        {
            if (mcOption.targetBtnText.text.Any() && !mcOption.targetBtnText.text.Equals("TargetSceneName"))
            {
                TargetScene sysMessage = new TargetScene(mcOption.labelBtnText.text, mcOption.targetBtnText.text, mcOption.hiddenDataText.text);
                bool isSysMessage = checkForSysMessage(sysMessage);
                if (!isSysMessage)
                {
                    saveController.loadScene(mcOption.targetBtnText.text);
                }
            }
        }
    }

    public void OnTargetButtonSelected(MultipleChoiceOption mcOption)
    {
        if (editModeController.isEditModeActive)
        {
            currentlyEditedOption = mcOption;
        }
    }
}
