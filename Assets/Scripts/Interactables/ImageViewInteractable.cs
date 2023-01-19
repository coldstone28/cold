using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    /// <summary>
    /// Like the investigate interactable, but with the image as trigger sprite, not the magnifier
    /// </summary>
    public class ImageViewInteractable : InvestigateInteractable
    {
        public Button triggerButton;

        protected override void Awake()
        {
            base.Awake();
            type = 7;
        }

        protected override void setImage(string imageName)
        {
            Sprite sp = ImageBuffer.getImageWithFileName(imageName);
            ButtonImage.GetComponent<Image>().sprite = sp;
            setTargetLabel(imageName);
            triggerButton.GetComponent<Image>().sprite = sp;
        }
        
        protected override void setImage(Sprite image, string imageName)
        {
            if (image)
            {
                ButtonImage.GetComponent<Image>().sprite = image;
                triggerButton.GetComponent<Image>().sprite = image;
                setTargetLabel(imageName);
            }
            else
            {
                Debug.Log("ERROR in ImageViewInteractable: Image not set");
            }
        }
    }
}