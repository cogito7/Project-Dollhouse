using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject[] cameras; // stores 2 cameras
    public GameObject[] players; //stores 2 players
    private int currentCameraIndex = 0; // Tracks the current camera
    void Start()
    {
        // Make sure to deactivate all cameras initially
        foreach (var camera in cameras)
        {
            camera.SetActive(false);
        }
        //Make sure to deactivate all players initially
        foreach(var player in players)
        {
            player.SetActive(false);
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
            players[i].SetActive(i == cameraIndex);
        }
    }
}
