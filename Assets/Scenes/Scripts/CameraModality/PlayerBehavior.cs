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
        _cameraTransform = Camera.main.transform;
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;//enable root motion
    }

    void Update()
    {
        if (CraftingUIManager.IsCraftingOpen) return;

        _isGrounded = CheckIsGrounded();//always updating ground check

        if (debug != null)
            debug.text = _isGrounded ? "Grounded" : "Not Grounded";

        _vInput = Input.GetAxis("Vertical");//player input y
        _hInput = Input.GetAxis("Horizontal");//player input x

        float moveAmount = new Vector2(_hInput, _vInput).magnitude;

        //animator parameters
        //animator.SetFloat("Speed", moveAmount, 0.1f, Time.deltaTime);
        


        if (moveAmount > 0.1f)
        {
            Vector3 v_flat = _cameraTransform.forward;
            v_flat.y = 0;
            v_flat = v_flat.normalized;

            Vector3 h_flat = _cameraTransform.right;
            h_flat.y = 0;
            h_flat = h_flat.normalized;

            Vector3 moveDirection = (v_flat * _vInput + h_flat * _hInput).normalized;

            Vector3 velocity = _rb.linearVelocity;
            velocity.x = moveDirection.x;
            velocity.z = moveDirection.z;
            _rb.linearVelocity = velocity;

            animator.SetBool("isMoving", true);
            RotateTowards(moveDirection);
        }
        else if (moveAmount < 0.1f)
        {
            animator.SetBool("isMoving", false);
        }


            _rb.linearDamping = _isGrounded ? groundDrag : 0f;

        // Optional jumping logic
        // if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        //     _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    private void OnAnimatorMove()
    {

            Vector3 rootMotion = animator.deltaPosition;
            Vector3 newPosition = _rb.position + rootMotion;

            // Use Rigidbody.MovePosition for smoother physics-based movement
            _rb.MovePosition(newPosition);

            // Apply root rotation if desired
            _rb.MoveRotation(animator.rootRotation);
        
    }

    void FixedUpdate()
    {
        if (CraftingUIManager.IsCraftingOpen) return;

        
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

        Debug.DrawRay(transform.position, Vector3.down * castDistance, Color.green);

        return Physics.Raycast(transform.position, Vector3.down, castDistance, groundLayers, QueryTriggerInteraction.Ignore);
    }
}