using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using static Unity.Burst.Intrinsics.X86;

public class Console : MonoBehaviour
{
    public UIButton uIButton;
    private void OnTriggerEnter(Collider other)
    {
        if (uIButton.step2_1 == true)
        {
            uIButton.Step2_2();
        }
    }
}
