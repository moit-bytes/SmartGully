using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
    public int mode = 0;                         //  0 -- Auto,  1 -- Manual
    public Animator anim;
    public GameObject butR;
    public GameObject butL;
    public GameObject onOff;

    void Start()
    {
        if(mode == 0)
        {
            butR.SetActive(false);
            butL.SetActive(true);
            slideR();

        }
        else
        {
            butR.SetActive(true);
            butL.SetActive(false);
            slideL();
        }


    }

    public void slideR()
    {
        anim.SetBool("slideR", true);
        butR.SetActive(true);
        butL.SetActive(false);
        onOff.SetActive(false);


    }

    public void slideL()
    {
        anim.SetBool("slideR", false);
        butR.SetActive(false);
        butL.SetActive(true);
        onOff.SetActive(true);


    }
}
