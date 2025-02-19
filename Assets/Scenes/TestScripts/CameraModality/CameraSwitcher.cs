using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject[] cameras; // stores 2 cameras
    private int currentCameraIndex = 0; // Tracks the current camera
    void Start()
    {
        // Make sure to deactivate all cameras initially
        foreach (var camera in cameras)
        {
            camera.SetActive(false);
        }

        // Activate the first camera at the start
        SwitchToCamera(0);
    }
    void Update()
    {
        // Check for the 'C' key press to switch cameras
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length; // Switch to next camera
            SwitchToCamera(currentCameraIndex);
        }
    }

    public void SwitchToCamera(int cameraIndex)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].SetActive(i == cameraIndex);
        }
    }
}
