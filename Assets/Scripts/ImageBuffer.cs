using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
    public class ImageBuffer : MonoBehaviour
    {
        private AssetLoader assetloader;
        private List<KeyValuePair<string, Sprite>> imageBuffer = new List<KeyValuePair<string, Sprite>>();

        internal bool isLoadingBuffer { get; private set; }
        
        private void Start()
        {
            isLoadingBuffer = true;
            GameObject[] sceneManagerObjects = GameObject.FindGameObjectsWithTag("SceneManager");
            foreach (var sceneManagerObject in sceneManagerObjects)
            {
                if (sceneManagerObject.GetComponent<AssetLoader>())
                {
                    assetloader = sceneManagerObject.GetComponent<AssetLoader>();
                    break;
                }
            }

            if (assetloader)
            {
                
            }
            else
            {
                Debug.Log("Error in ImageBuffer: No AssetLoader found");
            }
        }

        internal void Initialize()
        {
            StartCoroutine(LoadImageBuffer());
        }
        
        private IEnumerator LoadImageBuffer()
        {
            isLoadingBuffer = true;
            string[] imageFiles = assetloader.getAllPngImageFiles();
            foreach (var imageFile in imageFiles)
            {
                byte[] imgData;
                Texture2D tex = new Texture2D(2, 2);
                imgData = assetloader.getBytesFromImageFile(imageFile);
                tex.LoadImage(imgData);
                Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                imageBuffer.Add(new KeyValuePair<string, Sprite>(imageFile, sp));
            }
            yield return isLoadingBuffer = false;
        }

        public Sprite getImageWithFileName(string imageName)
        {
            foreach (var keyValuePair in imageBuffer)
            {
                if (keyValuePair.Key.Equals(imageName))
                {
                    return keyValuePair.Value;
                }
            }
            Debug.Log("Image not found in Buffer: " + imageName);
            return null;
        }

        internal int getBufferSize()
        {
            return imageBuffer.Count;
        }
        
        public KeyValuePair<string, Sprite> getImageWithIndex(int index)
        {
            if (index < imageBuffer.Count && index >= 0)
            {
                return imageBuffer[index];
            }
            Debug.Log("ImageBuffer index not valid: " + index);
            return new KeyValuePair<string, Sprite>();
        }
    }
}