using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{
    public GameObject test;

    public void pressMe()
    {
        test.SetActive(true);
    }
}
