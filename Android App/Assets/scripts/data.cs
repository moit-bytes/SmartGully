
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class data : MonoBehaviour
{
    public Text Active;
    void Start()
    {
        StartCoroutine(ledStatus("http://10.1.46.41:8002/ledstatus"));
    }

    IEnumerator ledStatus(string uri)
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
            powerC powerConsumption = (powerC)JsonUtility.FromJson(uwr.downloadHandler.text, typeof(powerC));

            Debug.Log(activity.isActive);
        }
    }
}
public class ledStatus
{
    public bool isActive;
}

public class powerC
{
    public float power;
}
