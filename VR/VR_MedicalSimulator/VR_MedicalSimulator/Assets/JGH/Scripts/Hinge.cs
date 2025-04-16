using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hinge : MonoBehaviour
{
    public PressureXRUIController pressureXRUIController;
    private Vector3 hingePosition;
    private Quaternion hingeRotation;

    void Start()
    {
        hingePosition = this.transform.position;
        hingeRotation = this.transform.rotation;
    }

    void FixedUpdate()
    {
        if (pressureXRUIController.PressureMain < 0f)
        {
            this.transform.rotation = hingeRotation;
        }
        else if (pressureXRUIController.PressureMain < 0.33f)
        {
            this.transform.rotation = hingeRotation * Quaternion.Euler(0f, 0f, 270f * pressureXRUIController.PressureMain);
        }
        else if (pressureXRUIController.PressureMain >= 0.33f)
        {
            this.transform.rotation = hingeRotation * Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
