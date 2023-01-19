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
            Debug.Log("Taste Q Gedrückt ");
            ButtonQ.GetComponent<Animator>().Play("Button-Q", -1, 0f);
            ButtonQ.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonW.GetComponent<Collider>())
        {
            Debug.Log("Taste W Gedrückt ");
            ButtonW.GetComponent<Animator>().Play("Button-W", -1, 0f);
            ButtonW.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonE.GetComponent<Collider>())
        {
            Debug.Log("Taste E Gedrückt ");
            ButtonE.GetComponent<Animator>().Play("Button-E", -1, 0f);
            ButtonE.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonR.GetComponent<Collider>())
        {
            Debug.Log("Taste R Gedrückt ");
            ButtonR.GetComponent<Animator>().Play("Button-R", -1, 0f);
            ButtonR.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonT.GetComponent<Collider>())
        {
            Debug.Log("Taste T Gedrückt ");
            ButtonT.GetComponent<Animator>().Play("Button-T", -1, 0f);
            ButtonT.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonZ.GetComponent<Collider>())
        {
            Debug.Log("Taste Z Gedrückt ");
            ButtonZ.GetComponent<Animator>().Play("Button-Z", -1, 0f);
            ButtonZ.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonU.GetComponent<Collider>())
        {
            Debug.Log("Taste U Gedrückt ");
            ButtonU.GetComponent<Animator>().Play("Button-U", -1, 0f);
            ButtonU.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonI.GetComponent<Collider>())
        {
            Debug.Log("Taste I Gedrückt ");
            ButtonI.GetComponent<Animator>().Play("Button-I", -1, 0f);
            ButtonI.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonO.GetComponent<Collider>())
        {
            Debug.Log("Taste O Gedrückt ");
            ButtonO.GetComponent<Animator>().Play("Button-O", -1, 0f);
            ButtonO.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonP.GetComponent<Collider>())
        {
            Debug.Log("Taste P Gedrückt ");
            ButtonP.GetComponent<Animator>().Play("Button-P", -1, 0f);
            ButtonP.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonA.GetComponent<Collider>())
        {
            Debug.Log("Taste A Gedrückt ");
            ButtonA.GetComponent<Animator>().Play("Button-A", -1, 0f);
            ButtonA.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonS.GetComponent<Collider>())
        {
            Debug.Log("Taste S Gedrückt ");
            ButtonS.GetComponent<Animator>().Play("Button-S", -1, 0f);
            ButtonS.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonD.GetComponent<Collider>())
        {
            Debug.Log("Taste D Gedrückt ");
            ButtonD.GetComponent<Animator>().Play("Button-D", -1, 0f);
            ButtonD.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonF.GetComponent<Collider>())
        {
            Debug.Log("Taste F Gedrückt ");
            ButtonF.GetComponent<Animator>().Play("Button-F", -1, 0f);
            ButtonF.GetComponent<VirtualKeyListener>().fireButton();
        }   
            if (other == ButtonG.GetComponent<Collider>())
        {   
            Debug.Log("Taste G Gedrückt ");
            ButtonG.GetComponent<Animator>().Play("Button-G", -1, 0f);
            ButtonG.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonH.GetComponent<Collider>())
        {
            Debug.Log("Taste H Gedrückt ");
            ButtonH.GetComponent<Animator>().Play("Button-H", -1, 0f);
            ButtonH.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonJ.GetComponent<Collider>())
        {
            Debug.Log("Taste J Gedrückt ");
            ButtonJ.GetComponent<Animator>().Play("Button-J", -1, 0f);
            ButtonJ.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonK.GetComponent<Collider>())
        {
            Debug.Log("Taste K Gedrückt ");
            ButtonK.GetComponent<Animator>().Play("Button-K", -1, 0f);
            ButtonK.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonL.GetComponent<Collider>())
        {
            Debug.Log("Taste L Gedrückt ");
            ButtonL.GetComponent<Animator>().Play("Button-L", -1, 0f);
            ButtonL.GetComponent<VirtualKeyListener>().fireButton();
        }   
            if (other == ButtonY.GetComponent<Collider>())
        {
            Debug.Log("Taste Y Gedrückt ");
            ButtonY.GetComponent<Animator>().Play("Button-Y", -1, 0f);
            ButtonY.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonX.GetComponent<Collider>())
        {
            Debug.Log("Taste X Gedrückt ");
            ButtonX.GetComponent<Animator>().Play("Button-X", -1, 0f);
            ButtonX.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonC.GetComponent<Collider>())
        {
            Debug.Log("Taste C Gedrückt ");
            ButtonC.GetComponent<Animator>().Play("Button-C", -1, 0f);
            ButtonC.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonV.GetComponent<Collider>())
        {
            Debug.Log("Taste V Gedrückt ");
            ButtonV.GetComponent<Animator>().Play("Button-V", -1, 0f);
            ButtonV.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonB.GetComponent<Collider>())
        {
            Debug.Log("Taste B Gedrückt ");
            ButtonB.GetComponent<Animator>().Play("Button-B", -1, 0f);
            ButtonB.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonN.GetComponent<Collider>())
        {
            Debug.Log("Taste N Gedrückt ");
            ButtonN.GetComponent<Animator>().Play("Button-N", -1, 0f);
            ButtonN.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonM.GetComponent<Collider>())
        {
            Debug.Log("Taste M Gedrückt ");
            ButtonM.GetComponent<Animator>().Play("Button-M", -1, 0f);
            ButtonM.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button_.GetComponent<Collider>())
        {
            Debug.Log("Taste _ Gedrückt ");
            Button_.GetComponent<Animator>().Play("Button-UNDERLINE", -1, 0f);
            Button_.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonUppercaseLeft.GetComponent<Collider>())
        {
            Debug.Log("Taste SHIFT-Links Gedrückt ");
            ButtonUppercaseLeft.GetComponent<Animator>().Play("Button-UPPERCASELEFT", -1, 0f);
            ButtonUppercaseLeft.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonUppercaseRight.GetComponent<Collider>())
        {
            Debug.Log("Taste SHIFT-Rechts Gedrückt ");
            ButtonUppercaseRight.GetComponent<Animator>().Play("Button-UPPERCASERIGHT", -1, 0f);
            ButtonUppercaseRight.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonAT.GetComponent<Collider>())
        {
            Debug.Log("Taste @ Gedrückt ");
            ButtonAT.GetComponent<Animator>().Play("Button-@", -1, 0f);
            ButtonAT.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonSPACE.GetComponent<Collider>())
        {
            Debug.Log("Taste Space Gedrückt ");
            ButtonSPACE.GetComponent<Animator>().Play("Button-SPACE", -1, 0f);
            ButtonSPACE.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonHYPHEN.GetComponent<Collider>())
        {
            Debug.Log("Taste Hyphen Gedrückt ");
            ButtonHYPHEN.GetComponent<Animator>().Play("Button-HYPHEN", -1, 0f);
            ButtonHYPHEN.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonCOMMA.GetComponent<Collider>())
        {
            Debug.Log("Taste Comma Gedrückt ");
            ButtonCOMMA.GetComponent<Animator>().Play("Button-COMMA", -1, 0f);
            ButtonCOMMA.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonPOINT.GetComponent<Collider>())
        {
            Debug.Log("Taste Point Gedrückt ");
            ButtonPOINT.GetComponent<Animator>().Play("Button-POINT", -1, 0f);
            ButtonPOINT.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonEXCLAMATIONMARK.GetComponent<Collider>())
        {
            Debug.Log("Taste Exclamation Gedrückt ");
            ButtonEXCLAMATIONMARK.GetComponent<Animator>().Play("Button-EXCLAMATIONMARK", -1, 0f);
            ButtonEXCLAMATIONMARK.GetComponent<VirtualKeyListener>().fireButton();
        }
        if (other == ButtonQUESTIONMARK.GetComponent<Collider>())
        {
            Debug.Log("Taste Questionmark Gedrückt ");
            ButtonQUESTIONMARK.GetComponent<Animator>().Play("Button-QUESTIONMARK", -1, 0f);
            ButtonQUESTIONMARK.GetComponent<VirtualKeyListener>().fireButton();
        }
        //####################################### ZAHLENREIHE ###################################
        if (other == ButtonESC.GetComponent<Collider>())
        {
            Debug.Log("Taste ESCAPE Gedrückt ");
            ButtonESC.GetComponent<Animator>().Play("Button-ESC", -1, 0f);
            ButtonESC.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button1.GetComponent<Collider>())
        {
            Debug.Log("Taste 1 Gedrückt ");
            Button1.GetComponent<Animator>().Play("Button-1", -1, 0f);
            Button1.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button2.GetComponent<Collider>())
        {
            Debug.Log("Taste 2 Gedrückt ");
            Button2.GetComponent<Animator>().Play("Button-2", -1, 0f);
            Button2.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button3.GetComponent<Collider>())
        {
            Debug.Log("Taste 3 Gedrückt ");
            Button3.GetComponent<Animator>().Play("Button-3", -1, 0f);
            Button3.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button4.GetComponent<Collider>())
        {
            Debug.Log("Taste 4 Gedrückt ");
            Button4.GetComponent<Animator>().Play("Button-4", -1, 0f);
            Button4.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button5.GetComponent<Collider>())
        {
            Debug.Log("Taste 5 Gedrückt ");
            Button5.GetComponent<Animator>().Play("Button-5", -1, 0f);
            Button5.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button6.GetComponent<Collider>())
        {
            Debug.Log("Taste 6 Gedrückt ");
            Button6.GetComponent<Animator>().Play("Button-6", -1, 0f);
            Button6.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button7.GetComponent<Collider>())
        {
            Debug.Log("Taste 7 Gedrückt ");
            Button7.GetComponent<Animator>().Play("Button-7", -1, 0f);
            Button7.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button8.GetComponent<Collider>())
        {
            Debug.Log("Taste 8 Gedrückt ");
            Button8.GetComponent<Animator>().Play("Button-8", -1, 0f);
            Button8.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button9.GetComponent<Collider>())
        {
            Debug.Log("Taste 9 Gedrückt ");
            Button9.GetComponent<Animator>().Play("Button-9", -1, 0f);
            Button9.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == Button0.GetComponent<Collider>())
        {
            Debug.Log("Taste 0 Gedrückt ");
            Button0.GetComponent<Animator>().Play("Button-0", -1, 0f);
            Button0.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonBACKSPACE.GetComponent<Collider>())
        {
            Debug.Log("Taste BACKSPACE Gedrückt ");
            ButtonBACKSPACE.GetComponent<Animator>().Play("Button-BACKSPACE", -1, 0f);
            ButtonBACKSPACE.GetComponent<VirtualKeyListener>().fireButton();
        }
            if (other == ButtonRETURN.GetComponent<Collider>())
        {
            Debug.Log("Taste RETURN Gedrückt ");
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
