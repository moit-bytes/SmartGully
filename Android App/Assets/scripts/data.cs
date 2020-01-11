
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class data : MonoBehaviour
{
    public Text Active;
    public static double powerValue;
    void Start()
    {
        StartCoroutine(ledStatus("http://10.1.46.41:8002/ledstatus"));
        StartCoroutine(powerConsp("http://10.1.46.41:8002/power"));
        //StartCoroutine(PostRequest("http://10.1.46.41:8002/mode"));
    }

    public void Update()
    {
    }

    IEnumerator ledStatus(string uri)
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
                ledStatus activity = (ledStatus)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(ledStatus));
            }
            yield return new WaitForSeconds(60f);
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
            yield return new WaitForSeconds(2f);

        }
    }


    IEnumerator PostRequest(string url)
    {

        WWWForm form = new WWWForm();
        form.AddField("mainMode", animations.mainMode);
        form.AddField("ledMode", animationsOnOff.ledMode);


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
