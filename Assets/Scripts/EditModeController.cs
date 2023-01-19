using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UI.SaveSystem;

public class EditModeController : MonoBehaviour
{
    public bool isEditModeActive;

    public Canvas[] ObservedCanvas;

    private UIController uiController;

    private SaveController saveController;

    private VideoController videoController;

    public UnityEvent OnStateChanged;
    
    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
        if (!uiController)
        {
            Debug.LogError("Error in EditModeController: Couldn't find UIController in Scene");
        }
        
        saveController = FindObjectOfType<SaveController>();
        if (!saveController)
        {
            Debug.LogError("Error in EditModeController: Couldn't find SaveController in Scene");
        }
        
        videoController = FindObjectOfType<VideoController>();
        if (!videoController)
        {
            Debug.LogError("Error in EditModeController: Couldn't find VideoController in Scene");
        }
    }

    public void toggleEditUI(bool active)
    {
        foreach (var canvas in ObservedCanvas)
        {
            TapButton[] buttons = canvas.GetComponentsInChildren<TapButton>();
            foreach (var button in buttons)
            {
                button.interactable = active;
            }
            
            Slider[] sliders = canvas.GetComponentsInChildren<Slider>();
            foreach (var slider in sliders)
            {
                slider.interactable = active;
            }
        }
        isEditModeActive = active;
        OnStateChanged.Invoke();
        videoController.startPlayingFromZero();
    }

    public void setCurrentSceneName()
    {
        uiController.openGenericInputField(setCurrentSceneName);
    }
    
    private void setCurrentSceneName(string sceneName)
    {
        saveController.renameCurrentScene(sceneName);
    }
}
