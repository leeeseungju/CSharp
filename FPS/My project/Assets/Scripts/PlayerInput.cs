using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string JumpButtonName = "Jump";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";

    public float move { get; private set; } 
    public float rotate { get; private set; }
    public bool jump { get; private set; }

    public bool fire { get; private set; } 
    public bool reload { get; private set; } 

    void Update()
    {
        float horizontalInput = Input.GetAxis(horizontalAxisName);
        float verticalInput = Input.GetAxis(verticalAxisName);

        move = Mathf.Sqrt(horizontalInput * horizontalInput + verticalInput * verticalInput);

        if (Input.GetKey(KeyCode.A))
        {
            move = -Mathf.Abs(move); //abs = Àý´ñ°ª
        }
        move = Input.GetAxis(horizontalAxisName);
        jump = Input.GetButton(JumpButtonName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
    }
}