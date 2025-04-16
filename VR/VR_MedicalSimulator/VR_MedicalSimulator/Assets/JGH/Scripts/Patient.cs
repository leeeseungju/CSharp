using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    public UIButton uIButton;
    public Transform[] destinations;
    public Quaternion[] rotations;

    public float moveSpeed = 5.0f;

    private Animator animator;
    private int currentIndex = 0;
    private Transform nowTransform;

    private float startTime;
    private float duration;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", false);
        animator.SetBool("Sit", false);
        animator.SetBool("Stand", false);
    }

    public void StartCoroutineSit()
    {
        animator.SetBool("Walk", true);
        StartCoroutine(MoveToDestinations());
    }

    public void Stand()
    {
        animator.SetBool("Sit", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Stand", true);
    }

    public IEnumerator MoveToDestinations()
    {
        while (currentIndex < destinations.Length)
        {
            Vector3 destinationPosition = destinations[currentIndex].position;
            Quaternion destinationRotation = rotations[currentIndex];
            Vector3 moveDirection = destinationPosition - transform.position;
            float distance = moveDirection.magnitude;

            Vector3 moveVector = moveDirection.normalized * moveSpeed * Time.deltaTime;

            transform.Translate(moveVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, destinationRotation, 0.1f);

            if (distance < 0.1f)
            {
                currentIndex++;
            }

            yield return null;
        }
        animator.SetBool("Walk", false);
        nowTransform = this.transform;

        yield return new WaitForSeconds(0.5f);

        startTime = Time.time;
        duration = 1.1f;

        while (Time.time - startTime < duration)
        {
            nowTransform.rotation = nowTransform.rotation * Quaternion.Euler(0f, 90f*Time.fixedDeltaTime, 0f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Sit", true);
        yield return new WaitForSeconds(1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (uIButton.step1 == true)
        {
            uIButton.processText.text = "환자를 좌석으로 안내하세요.";
            uIButton.step1_Button.SetActive(true);
            uIButton.StartTyping();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (uIButton.step1 == true)
        {
            uIButton.processText.text = "환자 옆으로 이동하세요.";
            uIButton.step1_Button.SetActive(false);
            uIButton.StartTyping();
        }
    }
}