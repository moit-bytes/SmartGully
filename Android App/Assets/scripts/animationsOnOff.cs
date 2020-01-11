using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsOnOff : MonoBehaviour
{
    public static int ledMode = 1;                         //  0 -- off,  1 -- on
    public Animator anim;
    public GameObject butR;
    public GameObject butL;

    void Start()
    {

        if(ledMode == 1)
        {
            butR.SetActive(true);
            butL.SetActive(false);
            slideR();

        }
        else
        {
            butR.SetActive(false);
            butL.SetActive(true);
            slideL();
        }


    }

    public void slideR()
    {
        anim.SetBool("onOffslideR", false);
        butR.SetActive(true);
        butL.SetActive(false);
        ledMode = 1;


    }

    public void slideL()
    {
        anim.SetBool("onOffslideR", true);
        butR.SetActive(false);
        butL.SetActive(true);
        ledMode = 0;


    }
}
