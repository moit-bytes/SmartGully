using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;




public class login : MonoBehaviour
{
    public Text username;
    public InputField password;
    void Start()
    {


    }

    public void loginButton()
    {
        StartCoroutine(PostRequest("http://gullyar.ankuranant.dev/auth"));
        Debug.Log(username.text);
        Debug.Log(password.text);
    }


    void Update()
    {
        
    }

    IEnumerator PostRequest(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username.text);
        form.AddField("password", password.text);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {

            loginValues user = (loginValues)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(loginValues));
            if(user.isLoggedIn)
            {
                PlayerPrefs.SetString("loginToken", user.token);
                PlayerPrefs.SetString("username", user.username);
                PlayerPrefs.SetString("location", user.location);
                SceneManager.LoadScene("app");
            }


        }
    }
}

[System.Serializable]
public class loginValues
{
    public string token;
    public string username;
    public string location;
    public bool isLoggedIn;
}
