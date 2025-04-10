using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpForce = 5f;

    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    private Transform _cameraTransform;
    private float _mouseX;
    public float groundDrag = 4f; // prevent sliding on the ground with friction
    private bool _isGrounded;
    public Animator animator;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform; // Gets the main camera location
        _rb.linearDamping = groundDrag; //apply grounding to reduce sliding
        animator = GetComponent<Animator>();//get animator component
    }

    void Update()
    {
        if (CraftingUIManager.IsCraftingOpen) return;
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f); //ground status

        // Get movement input
        _vInput = Input.GetAxis("Vertical"); // W & S (Forward & Back)
        _hInput = Input.GetAxis("Horizontal"); //A & D (Left & Right)

        //animator controller input movement
        float moveAmount = new Vector2(_hInput, _vInput).magnitude;
        animator.SetFloat("Speed", moveAmount);

        // checks if user presses spacebar for jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        // Mouse-controlled rotation (when right mouse button is held)
        //if (Input.GetMouseButton(1)) // right-click to rotate
        //{
        //    _mouseX += Input.GetAxis("Mouse X") * MouseSensitivity;
        //}
    }

    void FixedUpdate()
    {
        // Move the player based on user's WASD input
        Vector3 v_flat = _cameraTransform.forward;
        v_flat.y = 0;
        v_flat = v_flat.normalized;
        Vector3 h_flat = _cameraTransform.right;
        h_flat.y = 0;
        h_flat = h_flat.normalized;
        Vector3 moveDirection = (v_flat * _vInput + h_flat * _hInput).normalized * MoveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + moveDirection);
;
        if (moveDirection.sqrMagnitude > 0)
        {
            RotateTowards(moveDirection);
        }
        else
        {
            //RotateTowards(_rb.rotat);
        }
    }

    // Smooth rotation towards a direction
    void RotateTowards(Vector3 targetDirection)
    {
        // Calculate the rotation needed to face the target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Smoothly rotate the Rigidbody towards the target direction
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, 6f * Time.deltaTime);
    }

    // Checks if the player is on the ground
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
