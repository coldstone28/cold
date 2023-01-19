using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.SaveSystem;

public class InvestigateInteractable : Base360Interactable
{
    public GameObject ImageBox;
    public Image ButtonImage;
    private AssetLoader assetloader;
    private int currentImageIndex = 0;
    public GameObject ButtonNext;
    public GameObject ButtonPrev;
    protected ImageBuffer ImageBuffer;

    internal override int type { get; set; }

    protected override void Awake()
    {
        targetScenes = new List<TargetScene>();
        targetScenes.Add(new TargetScene("", "", ""));
        type = 5;
        
        GameObject[] sceneManagerObjects = GameObject.FindGameObjectsWithTag("SceneManager");
        foreach (var sceneManagerObject in sceneManagerObjects)
        {
            if (sceneManagerObject.GetComponent<ImageBuffer>())
            {
                ImageBuffer = sceneManagerObject.GetComponent<ImageBuffer>();
                break;
            }
        }
        
        base.Awake();
    }
    
    void Start()
    {
        if (!ImageBox)
        {
            Debug.Log("no ImageBox object set in InvestigateInteractable");
        }
        base.Start();
    }

    public void loadNextImage()
    {
        if (currentImageIndex < ImageBuffer.getBufferSize() - 1)
        {
            currentImageIndex++;
        }
        else
        {
            currentImageIndex = 0;
        }

        KeyValuePair<string, Sprite> image = ImageBuffer.getImageWithIndex(currentImageIndex);
        setImage(image.Value, image.Key);
    }

    public void loadPreviousImage()
    {
        if (currentImageIndex > 0)
        {
            currentImageIndex--;
        }
        else
        {
            currentImageIndex = ImageBuffer.getBufferSize() - 1;
        }
        
        KeyValuePair<string, Sprite> image = ImageBuffer.getImageWithIndex(currentImageIndex);
        setImage(image.Value, image.Key);
    }

    public void hideImage()
    {
        ImageBox.SetActive(false);
    }

    public void showImage()
    {
        ImageBox.SetActive(true);
    }

    protected virtual void setImage(string imageName)
    {
        if (imageName.Any())
        {
            Sprite sp = ImageBuffer.getImageWithFileName(imageName);
            ButtonImage.GetComponent<Image>().sprite = sp;
            setTargetLabel(imageName);
        }
        else
        {
            Debug.Log("ERROR in InvstigateInteractable: ImageName not set");
        }
    }
    
    protected virtual void setImage(Sprite image, string imageName)
    {
        if (image)
        {
            ButtonImage.GetComponent<Image>().sprite = image;
            setTargetLabel(imageName);
        }
        else
        {
            Debug.Log("ERROR in InvstigateInteractable: Image not set");
        }
    }
    
    public override void OnClicked()
    {
        showImage();
        if (editModeController.isEditModeActive)
        {
            ButtonNext.SetActive(true);
            ButtonPrev.SetActive(true);
        }
        else
        {
            ButtonNext.SetActive(false);
            ButtonPrev.SetActive(false);
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
            setImage(targetScenes[0].label);
            //SceneList.GetComponent<SceneList>().targetDisplay.text = targetScenes[0].targetSceneName;
        }
    }
}
