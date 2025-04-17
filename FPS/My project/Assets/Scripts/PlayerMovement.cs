using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float lookSensitivity = 5;
    public float cameraRotationLimit = 90f;
    private float currentCameraRotationX = 0;

    public Camera theCamera;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private PlayerInput playerInput;

    private bool isJumping = false;

    float hAxis;
    float vAxis;
    bool jDown;
    bool fDown;
    bool rDown;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        GetInput();
        Move();
        CameraRotation();
        CharacterRotation();

        if (playerInput.jump)
        {
            Jump();
        }
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButton("Fire1");
        rDown = Input.GetButtonDown("Reload");
    }

    private void Move()
    {
        bool isMoving = Mathf.Abs(hAxis) > 0.1f || Mathf.Abs(vAxis) > 0.1f;

        if (isMoving)
        {
            Vector3 _moveHorizontal = transform.right * hAxis;
            Vector3 _moveVertical = transform.forward * vAxis;

            Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * moveSpeed;

            playerRigidbody.MovePosition(transform.position + _velocity * Time.deltaTime);

            float moveInput = Mathf.Clamp(hAxis + vAxis, -1f, 1f);
            playerAnimator.SetFloat("Move", moveInput);
        }
        else
            playerAnimator.SetFloat("Move", 0f);

    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * 15, ForceMode.Impulse);
        playerAnimator.SetTrigger("Jump");
        isJumping = true;
    }
}
