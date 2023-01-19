using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;
using WebService;

namespace UnityEngine.UI.SaveSystem
{
    public class Authenticator : MonoBehaviour
    {
        public InputField inputMail;
        public InputField inputPassword;

        public UnityEvent<string> onLoggedIn;
        public UnityEvent onLoggedOut;
        public UnityEvent onLoginFailed;
        
        private void Start()
        {
            if (PlayerPrefs.GetInt("loggedIn") != 0)
            {
                onLoggedIn.Invoke(PlayerPrefs.GetString("username"));
            }
        }
        
        public void LoginFired()
        {
            StartCoroutine(LogIn(inputMail.text, inputPassword.text));
        }

        private IEnumerator LogIn(string email, string password)
        {
            string url = PlayerPrefs.GetString("serverRoot") + "api/login_unity.php?login=1";

            WWWForm form = new WWWForm();
            form.AddField("email", email);
            form.AddField("passwort", password);

            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
                yield return www.SendWebRequest();
                
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    Debug.Log("Login failed");
                    onLoginFailed.Invoke();
                }
                else
                {
                    string[] lines = www.downloadHandler.text.Split(new[] {';'});
                    PlayerPrefs.SetInt("loggedIn", 1);
                    PlayerPrefs.SetString("username", lines[0]);
                    PlayerPrefs.SetString("userid", lines[1]);
                    onLoggedIn.Invoke(lines[0]);
                }
            }
        }
        
        public void LogOut()
        {
            PlayerPrefs.SetInt("loggedIn", 0);
            PlayerPrefs.SetString("username", "");
            PlayerPrefs.SetString("userid", "");
            onLoggedOut.Invoke();
        }
    }
}