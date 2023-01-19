using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGrab : MonoBehaviour
{
    public GameObject IndexFinger;
    public GameObject MiddleFinger;
    public GameObject RingFinger;
    public GameObject PinkyFinger;

    public GameObject LoginMenu;

    public bool Index;
    public bool Middle;
    public bool Ring;
    public bool Pinky;

    public bool GrabbedLoginMenu;

    private Vector3 PosLoginMenu;

    public void Start()
    {
        PosLoginMenu = LoginMenu.transform.position;        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other == IndexFinger.GetComponent<Collider>())
        {
            Index = true;
            Debug.Log("Zeigefinger getriggert!");
        }
        if (other == MiddleFinger.GetComponent<Collider>())
        {
            Middle = true;
            Debug.Log("Mittelfinger getriggert!");
        }
        if (other == RingFinger.GetComponent<Collider>())
        {
            Ring = true;
            Debug.Log("Ringfinger getriggert!");
        }
        if (other == PinkyFinger.GetComponent<Collider>())
        {
            Pinky = true;
            Debug.Log("Mittelfinger getriggert!");
        }
        if (Index && Middle && Ring && Pinky && !GrabbedLoginMenu)
        {
            GrabbedLoginMenu = true;
            LoginMenu.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
        }
        if(other == LoginMenu.GetComponent<Collider>() && GrabbedLoginMenu)
        {
            Debug.Log("Zurück zur ursprünglichen Position");
            LoginMenu.transform.position = PosLoginMenu;
            GrabbedLoginMenu = false;
        }
    }

}
