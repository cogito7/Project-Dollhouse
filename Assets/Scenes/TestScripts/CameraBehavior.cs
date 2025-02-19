using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 2.8f, -3.5f);
    private Transform _target;
    private Rigidbody _rb;

    void Start()
    {
        _target = GameObject.Find("Player").transform;
        _rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody is set up correctly
        if (_rb != null)
        {
            _rb.isKinematic = true;  // Camera should not be affected by external forces
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.useGravity = false;
        }
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.TransformPoint(CamOffset);
        _rb.MovePosition(desiredPosition); // Smooth movement using physics

        // Ensure the camera has a valid direction to look at
        Vector3 direction = _target.position - transform.position;
        if (direction != Vector3.zero) // Prevent zero vector error
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rb.MoveRotation(targetRotation);
        }
    }
}

