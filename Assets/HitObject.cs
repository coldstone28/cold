using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitObject : MonoBehaviour
{
    public GameObject Object;
    private bool CheckRay;
    private Animator animator;

    void Start()
    {
        animator = Object.GetComponent<Animator>();
    }
    void Update()
    {      
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward); 

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == Object.tag && !CheckRay)
                {
                    Debug.Log("Hover aktiviert");
                    Debug.Log(hit.collider.gameObject.name);
                    animator.enabled = true;
                    animator.Play("UserMenuHoverEnter", -1, 0f);
                    CheckRay = true;
                    
                } /*else  if (hit.collider.tag != Object.tag && CheckRay )   //if (hit.collider == !Object.GetComponent<Collider>())
                {
                    Debug.Log("Hover deaktiviert");                    
                    Debug.Log(hit.collider.gameObject.name);
                    animator.Play("UserMenuHoverExit", -1, 0f);
                    CheckRay = false;               
                }   

            */} else {
               // animator.Play("UserMenuHoverExit", -1, 0f);
                Debug.Log("Teil 3");
                //CheckRay = false;
        }

       // Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
       // Debug.DrawRay(transform.position, forward, Color.green);
    }
}
