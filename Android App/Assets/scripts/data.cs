
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;


public class data : MonoBehaviour
{
    public Text Active;
    public static double powerValue;
    public bool ledStatusValue;
    public string activityText = "Off";

    public Text updateTime;


    void Start()
    {
        StartCoroutine(ledStatus("https://gullyar.ankuranant.dev/ledstatus/ledactive"));
        StartCoroutine(powerConsp("https://gullyar.ankuranant.dev/power"));
/*        StartCoroutine(PostRequest("http://10.1.46.41:8002/mode"))*/;

    }

    public void Update()
    {
        if (ledStatusValue)
        {
            Active.text = "Active";
        }
        else
        {
            Active.text = "Off";
        }
    }

    IEnumerator ledStatus(string uri)
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("location", PlayerPrefs.GetString("location", null));


            UnityWebRequest uwr = UnityWebRequest.Post(uri, form);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                ledStatus activity = (ledStatus)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(ledStatus));
                Debug.Log("Activityyyyyyyyyyyyy: " + activity.isActive);
                ledStatusValue = activity.isActive;

            }

            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator powerConsp(string uri)
    {
        while (true)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                powerC powerConsumption = (powerC)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(powerC));
                powerValue = powerConsumption.power;

            }
            updateTime.text = System.DateTime.Now.ToString("HH:mm tt dd/MM/yyyy");
            yield return new WaitForSeconds(3f);

        }
    }


    public void modeButton()
    {
        StartCoroutine(PostRequest("http://10.1.46.41:8002/mode"));
    }


/*    public void ledButton()
    {
        StartCoroutine(PostRequest("http://10.1.46.41:8002/mode"));
    }*/



    IEnumerator PostRequest(string url)
    {
        while (true)
        {

            WWWForm form = new WWWForm();
            form.AddField("mainMode", animations.mainMode);
            form.AddField("ledMode", animationsOnOff.ledMode);
            Debug.Log(animations.mainMode);


            UnityWebRequest uwr = UnityWebRequest.Post(url, form);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {

                mode user = (mode)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(mode));


            }
        }   
    }
}

//class
[System.Serializable]
public class ledStatus
{
    public bool isActive;
}


[System.Serializable]
public class powerC
{
    public string _id;
    public double power;
    public string addedOn;
}


[System.Serializable]
public class mode
{
    public int mainMode;
    public int ledMode;

}
