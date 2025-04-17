using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; 

    public float sensitivity = 2.0f; 
    public float maxYAngle = 80.0f;
    public float moveSpeed = 5.0f;

    private float rotationX = 0;
    private PlayerInput playerInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += mouseY;
        rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);

        transform.Rotate(Vector3.up * mouseX);

        Vector3 moveDirection = -transform.forward;
        moveDirection.y = 0;
        moveDirection.Normalize();
        //PlayerMovement.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
