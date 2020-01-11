using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class uiScript : MonoBehaviour
{
    public Text wattValue;
    public Image wattBar;

    public GameObject overPower;

    public double currentPowerValue;
    public double displayValue;

    void Start()
    {
        overPower.SetActive(false);
        currentPowerValue = data.powerValue;
    }

    void Update()
    {
        if(displayValue > 0.8)
        {
            overPower.SetActive(true);
        }

        currentPowerValue = data.powerValue;
        displayValue = currentPowerValue / 500;
        wattValue.text = Math.Round(currentPowerValue, 2) + "W";
        wattBar.fillAmount = (float)displayValue;
    }
}
