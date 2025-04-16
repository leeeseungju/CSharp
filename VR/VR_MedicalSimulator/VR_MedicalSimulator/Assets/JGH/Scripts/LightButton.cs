using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class LightButton : MonoBehaviour
{
    public MainButton mainbutton;
    public JGHXRLever lightButton;

    private void Update()
    {
        if (mainbutton.JGHMainButton == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LightOn()
    {
        if(mainbutton.JGHMainButton == true)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void LightOff()
    {
            this.gameObject.SetActive(false);
    }
}
