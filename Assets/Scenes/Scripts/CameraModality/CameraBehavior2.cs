using UnityEngine;

public class CameraBehavior2 : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 1.8f, -3.5f);
    private float camera_distance;
    private Transform _target;
    private Rigidbody _rb;
    public float MouseSensitivity;
    private float yaw;
    private float pitch;

    void Start()
    {
        /*_target = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody>();
        // Ensure Rigidbody is set up correctly
        if (_rb != null)
        {
            _rb.isKinematic = true;  // Camera should not be affected by external forces
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.useGravity = false;
        }
        Vector3 desiredPosition = _target.position + CamOffset;
        _rb.MovePosition(desiredPosition);
        camera_distance = CamOffset.magnitude; */
        _target = GameObject.Find("Player2").transform;
        camera_distance = CamOffset.magnitude;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }
    private void Update()
    {
        // If the crafting UI is not open, allow mouse movement to affect the camera's rotation
        if (!CraftingUIManager.IsCraftingOpen)
        {
            // Get mouse input for yaw (horizontal) and pitch (vertical)
            yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;

            // Clamp pitch to avoid flipping the camera
            pitch = Mathf.Clamp(pitch, -30f, 60f);
        }
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    { /*
        if (_target == null) return;

        Vector3 newPos = _rb.position;
        Debug.Log("prenormal " + Input.GetAxis("Mouse X"));
        newPos -= Input.GetAxis("Mouse X") * MouseSensitivity * _rb.transform.right;
        newPos -= Input.GetAxis("Mouse Y") * MouseSensitivity * _rb.transform.up;
        if (newPos.y < 0.5f)
        {
            newPos.y = 0.5f;
        }
        if (newPos.y > 5)
        {
            newPos.y = 5;
        }

        Vector3 relative_position_xz = newPos - _target.position;
        relative_position_xz.y = 0;
        relative_position_xz = relative_position_xz.normalized * camera_distance;
        relative_position_xz += _target.position;
        relative_position_xz.y = newPos.y;
        _rb.MovePosition(relative_position_xz);

        // Ensure the camera has a valid direction to look at
        Vector3 direction = (_target.position + Vector3.up * 1.0f) - transform.position;
        if (direction != Vector3.zero) // Prevent zero vector error
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rb.MoveRotation(targetRotation);
        } */
        if (_target == null) return;


        // Apply yaw and pitch to rotate the camera
        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Calculate the desired camera position based on the rotation and offset
        Vector3 desiredPosition = _target.position + camRotation * CamOffset;

        // Move the camera to the desired position
        transform.position = desiredPosition;

        // Make sure the camera is always looking at the player
        transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
        /*
        // Get and apply mouse input
        yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion camRotation = Quaternion.Euler(pitch, yaw, 0f);

        // Calculate new position
        Vector3 desiredPosition = _target.position + camRotation * CamOffset;

        // Move camera and look at target
        transform.position = desiredPosition;
        transform.rotation = Quaternion.LookRotation((_target.position + Vector3.up * 1.0f) - transform.position);
    */
    }

}