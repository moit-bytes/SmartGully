using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animations : MonoBehaviour
{
    public static int mainMode = 1;                         //  1 -- Auto,  0 -- Manual
    public Animator anim;
    public GameObject butR;
    public GameObject butL;
    public GameObject onOff;

    void Start()
    {
        if(mainMode == 1)
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
        anim.SetBool("slideR", true);
        butR.SetActive(true);
        butL.SetActive(false);
        onOff.SetActive(false);
        mainMode = 1;


    }

    public void slideL()
    {
        anim.SetBool("slideR", false);
        butR.SetActive(false);
        butL.SetActive(true);
        onOff.SetActive(true);
        mainMode = 0;



    }
}
