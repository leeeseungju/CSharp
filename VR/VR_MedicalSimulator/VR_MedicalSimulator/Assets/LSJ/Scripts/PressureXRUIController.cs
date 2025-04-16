using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class PressureXRUIController : MonoBehaviour
{
    public MainButton mainbutton;

    public LSJXRKnob pressurePlusWheel;
    public LSJXRKnob pressureMinusWheel;
    public LSJXRGripButton emergencyButton;

    public Vector3 originalPosition;
    public Quaternion originalRotation;

    public float PressureMain;
    public float WheelSpeed;

    void Start()
    {
        this.originalPosition = transform.position;
        this.originalRotation = transform.rotation;

        WheelSpeed = 0.05f;
    }

    void FixedUpdate()
    {
        if (mainbutton.JGHMainButton == true) {

            if (PressureMain < 0f)
            {
                PressureMain = 0f;
            }
            else if (PressureMain < 1.0f)
            {
                PressureMain = PressureMain + pressurePlusWheel.value * Time.fixedDeltaTime * 0.1f - pressureMinusWheel.value * Time.fixedDeltaTime * 0.1f;
            }
            else if (PressureMain > 1.0f)
            {
                PressureMain = PressureMain - pressureMinusWheel.value * Time.fixedDeltaTime * 0.1f;
            }

            this.transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, 116f * PressureMain);
        }
        else
        {
            resetTransform();
            this.transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, 116f * PressureMain);
        }
    }

    public void resetTransform()
    {
        if(PressureMain > 0f)
        {
            PressureMain = PressureMain - 0.25f * Time.fixedDeltaTime;
        }
        else
        {
            PressureMain = 0f;
        }

        pressurePlusWheel.value = 0f;
        pressureMinusWheel.value = 0f;
    }

    public void emergencyStart()
    {
        StartCoroutine(emergencyResetCoroutine());
    }

    public void emergencyStop()
    {
        StopCoroutine(emergencyResetCoroutine());
    }

    IEnumerator emergencyResetCoroutine()
    {
        while (PressureMain > 0)
        {
            PressureMain -= 1.5f * Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        PressureMain = 0f;
    }
}