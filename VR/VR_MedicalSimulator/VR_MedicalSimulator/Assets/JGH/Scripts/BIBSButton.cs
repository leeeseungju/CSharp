using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class BIBSButton : MonoBehaviour
{
    public JGHXRLever BIBSbutton;
    public bool BIBS;
    public float oxygen = 18.70f;

    public void BIBSTrue()
    {
        BIBS = true;
    }

    public void BIBSFalse()
    {
        BIBS = false;
    }

    void FixedUpdate()
    {
        if(BIBS == true)
        {
            if(oxygen < 99.47f)
            {
                oxygen = oxygen + Time.fixedDeltaTime * 9f;
            }
            else
            {
                oxygen = 99.47f;
            }
        }
        else
        {
            if (oxygen > 18.7f)
            {
                oxygen = oxygen - Time.fixedDeltaTime * 9f;
            }
            else
            {
                oxygen = 18.7f;
            }
        }
    }
}
