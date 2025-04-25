using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpForce = 5f;

    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    public Animator animator;
    private Transform _cameraTransform;
    public float groundDrag = 4f;
    private bool _isGrounded;
    public float distToGround = 0.4f;
    public LayerMask groundLayers;
    public Text debug;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
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
        animator.SetFloat("Speed", moveAmount, 0.1f, Time.deltaTime);
        animator.SetBool("isMoving", moveAmount > 0.1f);

        // Optional jumping logic
        // if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        //     _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (CraftingUIManager.IsCraftingOpen) return;

        Vector3 v_flat = _cameraTransform.forward;
        v_flat.y = 0;
        v_flat = v_flat.normalized;

        Vector3 h_flat = _cameraTransform.right;
        h_flat.y = 0;
        h_flat = h_flat.normalized;

        Vector3 moveDirection = (v_flat * _vInput + h_flat * _hInput).normalized;

        Vector3 velocity = _rb.linearVelocity;
        velocity.x = moveDirection.x * MoveSpeed;
        velocity.z = moveDirection.z * MoveSpeed;
        _rb.linearVelocity = velocity;

        if (moveDirection.sqrMagnitude > 0.1f)
            RotateTowards(moveDirection);

        _rb.linearDamping = _isGrounded ? groundDrag : 0f;
    }

    void RotateTowards(Vector3 targetDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, RotateSpeed * Time.deltaTime);
    }

    private bool CheckIsGrounded()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        float castDistance = col.bounds.extents.y + distToGround;

        Debug.DrawRay(transform.position, Vector3.down * castDistance, Color.green);
        return Physics.Raycast(transform.position, Vector3.down, castDistance, groundLayers, QueryTriggerInteraction.Ignore);
    }
}