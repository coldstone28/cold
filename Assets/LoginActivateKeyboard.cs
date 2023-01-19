using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginActivateKeyboard : MonoBehaviour
{
    public GameObject IndexFingerRight;
    public GameObject Keyboard;
    public GameObject SetNamePanel;

    void OnTriggerEnter(Collider other)
    {
        if(other == IndexFingerRight.GetComponent<Collider>())
        {
            Keyboard.SetActive(true);
            SetNamePanel.SetActive(true);
        }   
    }

}
