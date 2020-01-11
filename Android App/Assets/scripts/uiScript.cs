using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class uiScript : MonoBehaviour
{
    public Text wattValue;
    public Image wattBar;
    public double currentPowerValue;
    public double displayValue;
    void Start()
    {
        currentPowerValue = data.powerValue;
    }

    void Update()
    {
        currentPowerValue = data.powerValue;
        displayValue = currentPowerValue / 50;
        wattValue.text = Math.Round(currentPowerValue, 2) + "W";
        wattBar.fillAmount = (float)displayValue;
    }
}
