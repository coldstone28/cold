using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VerifyPanel : MonoBehaviour
{
    public Text question;

    internal UnityEvent<bool> OnSubmit = new Toggle.ToggleEvent();

    private bool noIsTrue;

    internal void setNoAsSubmit()
    {
        noIsTrue = true;
    }

    private void Submit(bool choice)
    {
        OnSubmit.Invoke(choice);
        
        //reset for next question
        OnSubmit.RemoveAllListeners();
        noIsTrue = false;
        gameObject.SetActive(false);
    }

    public void selectYes()
    {
        if (noIsTrue)
        {
            Submit(false);
        }
        else
        {
            Submit(true);
        }
    }

    public void selectNo()
    {
        if (noIsTrue)
        {
            Submit(true);
        }
        else
        {
            Submit(false);
        }
    }
}
