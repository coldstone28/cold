using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DeleteModeSwitcher : MonoBehaviour
{
    public Gradient normalModeGradient;
    public Gradient deleteModeGradient;
    
    internal bool isDeleteModeEnabled;

    public void toggleDeleteMode(bool enableDeleteMode)
    {
        if (enableDeleteMode)
        {
            isDeleteModeEnabled = true;
            gameObject.GetComponent<XRInteractorLineVisual>().invalidColorGradient = deleteModeGradient;
        }
        else
        {
            enableNormalMode();
        }
    }

    public void enableNormalMode()
    {
        isDeleteModeEnabled = false;
        gameObject.GetComponent<XRInteractorLineVisual>().invalidColorGradient = normalModeGradient;
    }
}
