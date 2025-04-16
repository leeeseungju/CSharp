using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class MainButton : MonoBehaviour
{
    public bool JGHMainButton;

    public JGHXRLever AirButtonMain;
    public JGHXRLever AirButtonSub;
    public JGHXRLever BIBSButtonMain;
    public JGHXRLever BIBSButtonSub;
    public JGHXRLever LightButtonMain;
    public JGHXRLever LightButtonSub;

    public void MainButtonOn()
    {
        JGHMainButton = true;
    }

    public void MainButtonOff()
    {
        JGHMainButton = false;
    }

    private void Update()
    {
        if (JGHMainButton == false)
        {
            AirButtonMain.value = false;
            AirButtonSub.value = false;
            BIBSButtonMain.value = false;
            BIBSButtonSub.value = false;
            LightButtonMain.value = false;
            LightButtonSub.value = false;
        }
    }
}