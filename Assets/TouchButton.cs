using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebService;

public class TouchButton : MonoBehaviour
{
    public VirtualKeyboardInputField VirtualKeyboardInputField;

    public GameObject ButtonESC;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject Button5;
    public GameObject Button6;
    public GameObject Button7;
    public GameObject Button8;
    public GameObject Button9;
    public GameObject Button0;
    public GameObject ButtonBACKSPACE;

    public GameObject ButtonQ;
    public GameObject ButtonW;
    public GameObject ButtonE;
    public GameObject ButtonR;
    public GameObject ButtonT;
    public GameObject ButtonZ;
    public GameObject ButtonU;
    public GameObject ButtonI;
    public GameObject ButtonO;
    public GameObject ButtonP;

    public GameObject ButtonA;
    public GameObject ButtonS;
    public GameObject ButtonD;
    public GameObject ButtonF;
    public GameObject ButtonG;
    public GameObject ButtonH;
    public GameObject ButtonJ;
    public GameObject ButtonK;
    public GameObject ButtonL;


    public GameObject ButtonUppercaseLeft;
    public GameObject ButtonUppercaseRight;
    public GameObject ButtonY;
    public GameObject ButtonX;
    public GameObject ButtonC;
    public GameObject ButtonV;
    public GameObject ButtonB;
    public GameObject ButtonN;
    public GameObject ButtonM;
    public GameObject Button_;

    public GameObject ButtonAT;
    public GameObject ButtonSPACE;
    public GameObject ButtonHYPHEN;
    public GameObject ButtonCOMMA;
    public GameObject ButtonPOINT;
    public GameObject ButtonEXCLAMATIONMARK;
    public GameObject ButtonQUESTIONMARK;

    public GameObject ButtonRETURN;


    public GameObject Keyboard;
    public GameObject Input_Field_Email;

    private void OnTriggerEnter(Collider other)
    {
            if (other == ButtonQ.GetComponent<Collider>())
        {
            Debug.Log("Taste Q Gedr�ckt ");
            ButtonQ.GetComponent<Animator>().Play("Button-Q", -1, 0f);
            ButtonQ.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonW.GetComponent<Collider>())
        {
            Debug.Log("Taste W Gedr�ckt ");
            ButtonW.GetComponent<Animator>().Play("Button-W", -1, 0f);
            ButtonW.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonE.GetComponent<Collider>())
        {
            Debug.Log("Taste E Gedr�ckt ");
            ButtonE.GetComponent<Animator>().Play("Button-E", -1, 0f);
            ButtonE.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonR.GetComponent<Collider>())
        {
            Debug.Log("Taste R Gedr�ckt ");
            ButtonR.GetComponent<Animator>().Play("Button-R", -1, 0f);
            ButtonR.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonT.GetComponent<Collider>())
        {
            Debug.Log("Taste T Gedr�ckt ");
            ButtonT.GetComponent<Animator>().Play("Button-T", -1, 0f);
            ButtonT.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonZ.GetComponent<Collider>())
        {
            Debug.Log("Taste Z Gedr�ckt ");
            ButtonZ.GetComponent<Animator>().Play("Button-Z", -1, 0f);
            ButtonZ.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonU.GetComponent<Collider>())
        {
            Debug.Log("Taste U Gedr�ckt ");
            ButtonU.GetComponent<Animator>().Play("Button-U", -1, 0f);
            ButtonU.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonI.GetComponent<Collider>())
        {
            Debug.Log("Taste I Gedr�ckt ");
            ButtonI.GetComponent<Animator>().Play("Button-I", -1, 0f);
            ButtonI.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonO.GetComponent<Collider>())
        {
            Debug.Log("Taste O Gedr�ckt ");
            ButtonO.GetComponent<Animator>().Play("Button-O", -1, 0f);
            ButtonO.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonP.GetComponent<Collider>())
        {
            Debug.Log("Taste P Gedr�ckt ");
            ButtonP.GetComponent<Animator>().Play("Button-P", -1, 0f);
            ButtonP.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonA.GetComponent<Collider>())
        {
            Debug.Log("Taste A Gedr�ckt ");
            ButtonA.GetComponent<Animator>().Play("Button-A", -1, 0f);
            ButtonA.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonS.GetComponent<Collider>())
        {
            Debug.Log("Taste S Gedr�ckt ");
            ButtonS.GetComponent<Animator>().Play("Button-S", -1, 0f);
            ButtonS.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonD.GetComponent<Collider>())
        {
            Debug.Log("Taste D Gedr�ckt ");
            ButtonD.GetComponent<Animator>().Play("Button-D", -1, 0f);
            ButtonD.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonF.GetComponent<Collider>())
        {
            Debug.Log("Taste F Gedr�ckt ");
            ButtonF.GetComponent<Animator>().Play("Button-F", -1, 0f);
            ButtonF.GetComponent<VirtualKeyListener>().fireButton();
        }   
            if (other == ButtonG.GetComponent<Collider>())
        {   
            Debug.Log("Taste G Gedr�ckt ");
            ButtonG.GetComponent<Animator>().Play("Button-G", -1, 0f);
            ButtonG.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonH.GetComponent<Collider>())
        {
            Debug.Log("Taste H Gedr�ckt ");
            ButtonH.GetComponent<Animator>().Play("Button-H", -1, 0f);
            ButtonH.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonJ.GetComponent<Collider>())
        {
            Debug.Log("Taste J Gedr�ckt ");
            ButtonJ.GetComponent<Animator>().Play("Button-J", -1, 0f);
            ButtonJ.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonK.GetComponent<Collider>())
        {
            Debug.Log("Taste K Gedr�ckt ");
            ButtonK.GetComponent<Animator>().Play("Button-K", -1, 0f);
            ButtonK.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonL.GetComponent<Collider>())
        {
            Debug.Log("Taste L Gedr�ckt ");
            ButtonL.GetComponent<Animator>().Play("Button-L", -1, 0f);
            ButtonL.GetComponent<VirtualKeyListener>().fireButton();
        }   
            if (other == ButtonY.GetComponent<Collider>())
        {
            Debug.Log("Taste Y Gedr�ckt ");
            ButtonY.GetComponent<Animator>().Play("Button-Y", -1, 0f);
            ButtonY.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonX.GetComponent<Collider>())
        {
            Debug.Log("Taste X Gedr�ckt ");
            ButtonX.GetComponent<Animator>().Play("Button-X", -1, 0f);
            ButtonX.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonC.GetComponent<Collider>())
        {
            Debug.Log("Taste C Gedr�ckt ");
            ButtonC.GetComponent<Animator>().Play("Button-C", -1, 0f);
            ButtonC.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonV.GetComponent<Collider>())
        {
            Debug.Log("Taste V Gedr�ckt ");
            ButtonV.GetComponent<Animator>().Play("Button-V", -1, 0f);
            ButtonV.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonB.GetComponent<Collider>())
        {
            Debug.Log("Taste B Gedr�ckt ");
            ButtonB.GetComponent<Animator>().Play("Button-B", -1, 0f);
            ButtonB.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonN.GetComponent<Collider>())
        {
            Debug.Log("Taste N Gedr�ckt ");
            ButtonN.GetComponent<Animator>().Play("Button-N", -1, 0f);
            ButtonN.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonM.GetComponent<Collider>())
        {
            Debug.Log("Taste M Gedr�ckt ");
            ButtonM.GetComponent<Animator>().Play("Button-M", -1, 0f);
            ButtonM.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button_.GetComponent<Collider>())
        {
            Debug.Log("Taste _ Gedr�ckt ");
            Button_.GetComponent<Animator>().Play("Button-UNDERLINE", -1, 0f);
            Button_.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonUppercaseLeft.GetComponent<Collider>())
        {
            Debug.Log("Taste SHIFT-Links Gedr�ckt ");
            ButtonUppercaseLeft.GetComponent<Animator>().Play("Button-UPPERCASELEFT", -1, 0f);
            ButtonUppercaseLeft.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonUppercaseRight.GetComponent<Collider>())
        {
            Debug.Log("Taste SHIFT-Rechts Gedr�ckt ");
            ButtonUppercaseRight.GetComponent<Animator>().Play("Button-UPPERCASERIGHT", -1, 0f);
            ButtonUppercaseRight.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonAT.GetComponent<Collider>())
        {
            Debug.Log("Taste @ Gedr�ckt ");
            ButtonAT.GetComponent<Animator>().Play("Button-@", -1, 0f);
            ButtonAT.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonSPACE.GetComponent<Collider>())
        {
            Debug.Log("Taste Space Gedr�ckt ");
            ButtonSPACE.GetComponent<Animator>().Play("Button-SPACE", -1, 0f);
            ButtonSPACE.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonHYPHEN.GetComponent<Collider>())
        {
            Debug.Log("Taste Hyphen Gedr�ckt ");
            ButtonHYPHEN.GetComponent<Animator>().Play("Button-HYPHEN", -1, 0f);
            ButtonHYPHEN.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonCOMMA.GetComponent<Collider>())
        {
            Debug.Log("Taste Comma Gedr�ckt ");
            ButtonCOMMA.GetComponent<Animator>().Play("Button-COMMA", -1, 0f);
            ButtonCOMMA.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonPOINT.GetComponent<Collider>())
        {
            Debug.Log("Taste Point Gedr�ckt ");
            ButtonPOINT.GetComponent<Animator>().Play("Button-POINT", -1, 0f);
            ButtonPOINT.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonEXCLAMATIONMARK.GetComponent<Collider>())
        {
            Debug.Log("Taste Exclamation Gedr�ckt ");
            ButtonEXCLAMATIONMARK.GetComponent<Animator>().Play("Button-EXCLAMATIONMARK", -1, 0f);
            ButtonEXCLAMATIONMARK.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonQUESTIONMARK.GetComponent<Collider>())
        {
            Debug.Log("Taste Questionmark Gedr�ckt ");
            ButtonQUESTIONMARK.GetComponent<Animator>().Play("Button-QUESTIONMARK", -1, 0f);
            ButtonQUESTIONMARK.GetComponent<VirtualKeyListener>().fireButton();
        }
        //####################################### ZAHLENREIHE ###################################
        if (other == ButtonESC.GetComponent<Collider>())
        {
            Debug.Log("Taste ESCAPE Gedr�ckt ");
            ButtonESC.GetComponent<Animator>().Play("Button-ESC", -1, 0f);
            ButtonESC.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button1.GetComponent<Collider>())
        {
            Debug.Log("Taste 1 Gedr�ckt ");
            Button1.GetComponent<Animator>().Play("Button-1", -1, 0f);
            Button1.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button2.GetComponent<Collider>())
        {
            Debug.Log("Taste 2 Gedr�ckt ");
            Button2.GetComponent<Animator>().Play("Button-2", -1, 0f);
            Button2.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button3.GetComponent<Collider>())
        {
            Debug.Log("Taste 3 Gedr�ckt ");
            Button3.GetComponent<Animator>().Play("Button-3", -1, 0f);
            Button3.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button4.GetComponent<Collider>())
        {
            Debug.Log("Taste 4 Gedr�ckt ");
            Button4.GetComponent<Animator>().Play("Button-4", -1, 0f);
            Button4.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button5.GetComponent<Collider>())
        {
            Debug.Log("Taste 5 Gedr�ckt ");
            Button5.GetComponent<Animator>().Play("Button-5", -1, 0f);
            Button5.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button6.GetComponent<Collider>())
        {
            Debug.Log("Taste 6 Gedr�ckt ");
            Button6.GetComponent<Animator>().Play("Button-6", -1, 0f);
            Button6.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button7.GetComponent<Collider>())
        {
            Debug.Log("Taste 7 Gedr�ckt ");
            Button7.GetComponent<Animator>().Play("Button-7", -1, 0f);
            Button7.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button8.GetComponent<Collider>())
        {
            Debug.Log("Taste 8 Gedr�ckt ");
            Button8.GetComponent<Animator>().Play("Button-8", -1, 0f);
            Button8.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button9.GetComponent<Collider>())
        {
            Debug.Log("Taste 9 Gedr�ckt ");
            Button9.GetComponent<Animator>().Play("Button-9", -1, 0f);
            Button9.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button0.GetComponent<Collider>())
        {
            Debug.Log("Taste 0 Gedr�ckt ");
            Button0.GetComponent<Animator>().Play("Button-0", -1, 0f);
            Button0.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonBACKSPACE.GetComponent<Collider>())
        {
            Debug.Log("Taste BACKSPACE Gedr�ckt ");
            ButtonBACKSPACE.GetComponent<Animator>().Play("Button-BACKSPACE", -1, 0f);
            ButtonBACKSPACE.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonRETURN.GetComponent<Collider>())
        {
            Debug.Log("Taste RETURN Gedr�ckt ");
            ButtonRETURN.GetComponent<Animator>().Play("Button-RETURN", -1, 0f);
            ButtonRETURN.GetComponent<VirtualKeyListener>().fireButton();
        }

            if (other == Input_Field_Email.GetComponent<Collider>())
        {
            Debug.Log("Tastatur geoeffnet");
            VirtualKeyboardInputField.FireOnSelect();
            //Button_.GetComponent<Animator>().Play("Button-_", -1, 0f);
            //Button_.GetComponent<VirtualKeyListener>().fireButton();
        }
    }
}
