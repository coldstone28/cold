﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.SaveSystem;

public class ChecklistRootInteractable : Base360Interactable
{
    public GameObject SceneList;

    internal override int type { get; set; }

    protected override void Awake()
    {
        targetScenes = new List<TargetScene>();
        targetScenes.Add(new TargetScene("", "", ""));
        type = 13;
        base.Awake();
    }

    void Start()
    {
        if (!SceneList)
        {
            Debug.Log("no videoFileList object set in ChecklistRootInteractable");
        }
        base.Start();
    }
    
    public override void OnClicked()
    {
        if (editModeController.isEditModeActive)
        {
            if (SceneList.activeInHierarchy)
            {
                SceneList.SetActive(false);
            } 
            else
            {
                SceneList.SetActive(true);
                SceneList.GetComponent<SceneList>().reloadScenes();
            }
        }
        else
        {
            if (targetScenes[0].targetSceneName.Any())
            {
                bool isSysMessage = checkForSysMessage(targetScenes[0]);
                if (!isSysMessage)
                {
                    saveController.loadScene(targetScenes[0].targetSceneName);
                }
            }
        }
    }

    internal override void setTargetScene(string sceneName)
    {
        targetScenes[0].targetSceneName = sceneName;
    }

    internal override void setTargetLabel(string labelName)
    {
        targetScenes[0].label = labelName;
    }
    
    internal override void setTargetHiddenData(string stringData)
    {
        targetScenes[0].hiddenData = stringData;
    }

    internal override void updateGUI()
    {
        if (targetScenes[0] != null)
        {
            SceneList.GetComponent<SceneList>().targetDisplay.text = targetScenes[0].targetSceneName;
        }
    }
}
