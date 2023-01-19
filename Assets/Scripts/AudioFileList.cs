using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioFileList : MonoBehaviour
{
    private AssetLoader assetloader;

    private SaveController saveController;

    /* The interactable the audioFileList is attached to */
    public Base360Interactable interactable;

    /* Button in scene data canvas, that displays current 360°-video filename in scene */
    public Button currenAudiofileButton;
    
    /* for instancing new buttons with several filenames */
    public Button templateButton;

    public Image listGrid;

    public Text currentAudioDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] sceneManagerObjects = GameObject.FindGameObjectsWithTag("SceneManager");
        foreach (var sceneManagerObject in sceneManagerObjects)
        {
            Debug.Log("found");
            if (sceneManagerObject.GetComponent<AssetLoader>())
            {
                assetloader = sceneManagerObject.GetComponent<AssetLoader>();
                loadAudiofiles();
                saveController = sceneManagerObject.GetComponent<SaveController>();
                break;
            }
        }
    }

    private void loadAudiofiles()
    {
        string[] fileNames = assetloader.getAllAudioFiles();

        foreach (Transform child in listGrid.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var fileName in fileNames)
        {
            Button newEntry = Instantiate(templateButton);
            newEntry.onClick.AddListener(delegate
            {
                currenAudiofileButton.GetComponentInChildren<Text>().text = fileName;
                interactable.setTargetScene(fileName);
            });
            newEntry.GetComponentInChildren<Text>().text = fileName;
            newEntry.transform.SetParent(listGrid.transform, false);
        }
    }
}
