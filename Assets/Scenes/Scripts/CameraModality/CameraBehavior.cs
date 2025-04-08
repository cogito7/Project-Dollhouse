using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 2.2f, -3.5f);
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
        _target = GameObject.Find("Player").transform;
        camera_distance = CamOffset.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
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
    }

}

