using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class startUp : MonoBehaviour
{
    public GameObject loginScreen;

    void Start()
    {
        loginScreen.SetActive(false);
    }

    public void mainApp()
    {
        if (PlayerPrefs.HasKey("loginToken"))
        {
            SceneManager.LoadScene("app");


        }
        else
        {
            loginScreen.SetActive(true);

        }
    }
}
