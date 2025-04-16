using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepSound : MonoBehaviour
{
    [SerializeField] private InputActionReference m_moveReference;
    public AudioSource footStepSource;
    public AudioClip footStepClip;    

    private void Start()
    {
        footStepSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        m_moveReference.action.performed += Performed;
        m_moveReference.action.canceled += Cancel;
    }
    private void OnDisable()
    {
        m_moveReference.action.performed -= Performed;
        m_moveReference.action.canceled -= Cancel;
    }
    private void PlayFootstepSound()
    {
        if (footStepClip != null)
        {
            if(!footStepSource.isPlaying)
            {
                footStepSource.PlayOneShot(footStepClip);
            }
        }
    }
    private void StopFootstepSound()
    {
        if (!footStepSource.isPlaying)
        {
            footStepSource.Stop();
        }        
    }
    private void Performed(InputAction.CallbackContext obj) 
    {
        Debug.Log($"Move Action");
        PlayFootstepSound();
    }
    private void Cancel(InputAction.CallbackContext obj) 
    {
        Debug.Log($"Move Cancel");
        StopFootstepSound();
    }
}
