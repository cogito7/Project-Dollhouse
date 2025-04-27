using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior2 : MonoBehaviour
{
    public float RotateSpeed = 75f;
    public float JumpForce = 5f;

    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    private Transform _cameraTransform;
    public float groundDrag = 4f;
    public Animator animator;
    private bool _isGrounded;
    public float distToGround = 0.4f;
    public LayerMask groundLayers;
    public Text debug;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = GameObject.Find("Camera2").transform;
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    void Update()
    {
        if (CraftingUIManager.IsCraftingOpen) return;

        _isGrounded = CheckIsGrounded();

        if (debug != null)
            debug.text = _isGrounded ? "Grounded" : "Not Grounded";

        _vInput = Input.GetAxis("Vertical");
        _hInput = Input.GetAxis("Horizontal");

        float moveAmount = new Vector2(_hInput, _vInput).magnitude;

        if (moveAmount > 0.1f)
        {
            Vector3 v_flat = _cameraTransform.forward;
            v_flat.y = 0;
            v_flat.Normalize();

            Vector3 h_flat = _cameraTransform.right;
            h_flat.y = 0;
            h_flat.Normalize();

            Vector3 moveDirection = (v_flat * _vInput + h_flat * _hInput).normalized;

            Vector3 velocity = _rb.linearVelocity;
            velocity.x = moveDirection.x;
            velocity.z = moveDirection.z;
            _rb.linearVelocity = velocity;

            animator.SetBool("isMoving", true);
            RotateTowards(moveDirection);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        _rb.linearDamping = _isGrounded ? groundDrag : 0f;

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
        StepClimb();
    }

    void FixedUpdate()
    {
        if (CraftingUIManager.IsCraftingOpen) return;
        // Nothing needed here if using root motion
    }

    private void OnAnimatorMove()
    {
        Vector3 rootMotion = animator.deltaPosition;
        Vector3 newPosition = _rb.position + rootMotion;

        _rb.MovePosition(newPosition);
        _rb.MoveRotation(animator.rootRotation);
    }

    void RotateTowards(Vector3 targetDir)
    {
        if (targetDir == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
    }

    private bool CheckIsGrounded()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        float castDistance = col.bounds.extents.y + distToGround;

        Debug.DrawRay(transform.position, Vector3.down * castDistance, Color.cyan);
        return Physics.Raycast(transform.position, Vector3.down, castDistance, groundLayers, QueryTriggerInteraction.Ignore);
    }
    void StepClimb()
    {
        // Only check for stairs when grounded
        if (!_isGrounded) return;

        // Maximum height of a step to climb 
        float maxStepHeight = 1.4f; // Maximum step height the player can walk up
        float stepSmooth = 0.5f;  // How much to lift the player each time

        // Raycasting to check in front of the player for steps
        Vector3 origin = transform.position + Vector3.up * 0.1f;  // Start just above the ground (player's feet)
        Vector3 forward = transform.forward * 0.5f;  // Distance in front of the player to check
        RaycastHit hit;

        // Raycast downwards to check the surface height
        if (Physics.Raycast(origin + forward, Vector3.down, out hit, maxStepHeight, groundLayers))
        {
            // Calculate the height difference from the detected surface
            float stepHeight = hit.point.y - origin.y;

            // If the step height is within the acceptable range, help the player climb
            if (stepHeight > 0 && stepHeight <= maxStepHeight)
            {
                // Lift the player gently to climb the step
                _rb.position = new Vector3(_rb.position.x, _rb.position.y + stepSmooth, _rb.position.z);
            }
        }
    }
}