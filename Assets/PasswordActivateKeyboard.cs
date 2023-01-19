using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordActivateKeyboard : MonoBehaviour
{
    public GameObject IndexFingerRight;
    public GameObject Keyboard;

    void OnTriggerEnter(Collider other)
    {
        if (other == IndexFingerRight.GetComponent<Collider>())
        {
            Keyboard.SetActive(true);
        }
    }

}
