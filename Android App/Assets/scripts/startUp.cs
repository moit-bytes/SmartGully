using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;



public class startUp : MonoBehaviour
{
    public bool tokenValidd;
    public GameObject loginScreen;

    void Start()
    {
        loginScreen.SetActive(false);
        StartCoroutine(GetRequest("http://10.1.46.41:8002/auth/verify"));

    }

    public void mainApp()
    {
        if (tokenValidd)
        {
            SceneManager.LoadScene("app");


        }
        else
        {
            loginScreen.SetActive(true);

        }
    }
    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        uwr.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("loginToken", null));

        Debug.Log(PlayerPrefs.GetString("loginToken"));
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            tokenValidation tokenValid = (tokenValidation)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(tokenValidation));
            Debug.Log(tokenValid.isLoggedIn);
            tokenValidd = tokenValid.isLoggedIn;
        }
    }
}

[System.Serializable]
public class tokenValidation
{
    public bool isLoggedIn;
}
