using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace XRButtonhandling
{

    [System.Serializable]
    public class PrimaryButtonEvent : UnityEvent<ControllerPress> { }

    public enum ControllerEnum
    {
        Left, Right
    }

    public struct ControllerPress
    {
        public ControllerEnum pressedController;
        public InputFeatureUsage<bool> pressedButton;
        public bool isPressed;

        public ControllerPress (ControllerEnum controller, InputFeatureUsage<bool> button)
        {
            pressedController = controller;
            pressedButton = button;
            isPressed = true;
        }
    }

    public class XRButtonListener : MonoBehaviour
    {
        public PrimaryButtonEvent primaryButtonPress;
        public PrimaryButtonEvent primaryButtonRelease;

        private bool lastPrimaryButtonStateL = false;
        private bool isPrimaryButtonStatePressed = false;
        private List<InputDevice> devicesWithPrimaryButton;
        private InputDevice LeftController;
        private InputDevice RightController;

        private bool leftPressing = false;
        private bool rightPressing = false;

        private bool secondaryTouchValue;

        private void Awake()
        {
            if (primaryButtonPress == null)
            {
                primaryButtonPress = new PrimaryButtonEvent();
            }
            
            if (primaryButtonRelease == null)
            {
                primaryButtonRelease = new PrimaryButtonEvent();
            }

            devicesWithPrimaryButton = new List<InputDevice>();
        }

        void OnEnable()
        {
            List<InputDevice> allDevices = new List<InputDevice>();
            InputDevices.GetDevices(allDevices);
            foreach (InputDevice device in allDevices)
                InputDevices_deviceConnected(device);

            InputDevices.deviceConnected += InputDevices_deviceConnected;
            InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;


            foreach (InputDevice id in allDevices)
            {
                Debug.Log(id.name, this);
            }
        }

        private void OnDisable()
        {
            InputDevices.deviceConnected -= InputDevices_deviceConnected;
            InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
            devicesWithPrimaryButton.Clear();
        }

        private void InputDevices_deviceConnected(InputDevice device)
        {
            bool discardedValue;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out discardedValue))
            {
                devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
            }
            
            // store left and right controller
            if(device.TryGetFeatureValue(CommonUsages.gripButton, out discardedValue))
            {
                // TODO: maybe check for more buttons?
                if(device.name.Contains("Left"))
                {
                    LeftController = device;
                    Debug.Log("add left", this);
                }

                if (device.name.Contains("Right"))
                {
                    RightController = device;
                    Debug.Log("add left", this);
                }
            }

            Debug.Log("on connect", this);
            foreach (InputDevice id in devicesWithPrimaryButton)
            {
                Debug.Log(id.name, this);
            }
        }

        private void InputDevices_deviceDisconnected(InputDevice device)
        {
            if (devicesWithPrimaryButton.Contains(device))
                devicesWithPrimaryButton.Remove(device);
        }

        void Update()
        {
            bool tempState = false;

            // check for buttons for right
            tempState = CheckForButtonPress(RightController, CommonUsages.primaryButton);

            if (tempState && isPrimaryButtonStatePressed == false)
            {
                isPrimaryButtonStatePressed = true;
                primaryButtonPress.Invoke(new ControllerPress(ControllerEnum.Right, CommonUsages.primaryButton));
            }
            else if(!tempState && isPrimaryButtonStatePressed == true)
            {
                isPrimaryButtonStatePressed = false;
                primaryButtonRelease.Invoke(new ControllerPress(ControllerEnum.Right, CommonUsages.primaryButton));
            }
        }

        private bool CheckForButtonPress(InputDevice device, InputFeatureUsage<bool> button)
        {
            bool buttonState = false;
            return device.TryGetFeatureValue(button, out buttonState) // did get a value
                   && buttonState; // the actual value we got
        }

        private bool CheckForButtonPressByAxis(InputDevice device, InputFeatureUsage<float> button)
        {
            device.TryGetFeatureValue(button, out var buttonState);
            return (buttonState > 0.1f);
        }

        public bool GetIsLeftPressing()
        {
            return leftPressing;
        }

        public bool GetIsRightPressing()
        {
            return rightPressing;
        }
    }

}