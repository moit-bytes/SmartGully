using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsOnOff : MonoBehaviour
{
    public int mode = 0;                         //  0 -- on,  1 -- off
    public Animator anim;
    public GameObject butR;
    public GameObject butL;

    void Start()
    {

        if(mode == 0)
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


    }

    public void slideL()
    {
        anim.SetBool("onOffslideR", true);
        butR.SetActive(false);
        butL.SetActive(true);


    }
}
