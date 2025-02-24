using UnityEngine;

public class PlayerBehavior2 : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpForce = 5f;
    public float MouseSensitivity = 2f;

    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    private Transform _cameraTransform;
    private float _mouseX;
    public float groundDrag = 4f; // prevent sliding on the ground with friction
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = GameObject.Find("Camera2").transform; // Gets the 2nd camera location
        _rb.drag = groundDrag; //apply grounding to reduce sliding
    }

    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f); //ground status
        // Get movement input
        _vInput = Input.GetAxis("Vertical"); // W & S (Forward & Back)
        _hInput = Input.GetAxis("Horizontal"); //A & D (Left & Right)

        // checks if user presses spacebar for jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        // Mouse-controlled rotation (when right mouse button is held)
        if (Input.GetMouseButton(1)) // right-click to rotate
        {
            _mouseX += Input.GetAxis("Mouse X") * MouseSensitivity;
        }
    }

    void FixedUpdate()
    {
        // Move the player based on user's WASD input
        Vector3 moveDirection = (transform.forward * _vInput + transform.right * _hInput).normalized * MoveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + moveDirection);

        // Rotate player based on mouse movement
        transform.rotation = Quaternion.Euler(0, _mouseX, 0);

    }


    // Checks if the player is on the ground
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

}
