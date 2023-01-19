using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WebService
{
    public class ServerConfigurator : MonoBehaviour
    {
        public static string defaultServerRoot = "http://mixality.technik-emden.de/";
        public InputField InputField;

        private void Start()
        {
            if (PlayerPrefs.HasKey("serverRoot") && PlayerPrefs.GetString("serverRoot").Any())
            {
                InputField.text = PlayerPrefs.GetString("serverRoot");
            }
            else
            {
                InputField.text = defaultServerRoot;
                PlayerPrefs.SetString("serverRoot", defaultServerRoot);
            }
        }

        public void UpdateServerRoot()
        {
            string stringCheck = InputField.text;
            stringCheck = stringCheck.Replace('\\', '/');
            if (!stringCheck.Substring(stringCheck.Length - 1).Equals("/"))
            {
                stringCheck = stringCheck + "/";
            }
            PlayerPrefs.SetString("serverRoot", stringCheck);
            gameObject.SetActive(false);
        }
    }
}