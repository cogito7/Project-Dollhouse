using UnityEngine;
using UnityEngine.Video;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TVTrigger : MonoBehaviour
{
    public GameObject turnOn;
    public VideoPlayer video;
    public Transform triggerLocation;
    public GameObject[] players; // Assign both Player and Player2 in the Inspector
    public string itemRequired;
    public float activationRange = 2f;
    public Texture2D handCursor;
    public CraftingInventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool openHandCursor = false;
        // Press left mouse to activate
        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy && Vector3.Distance(triggerLocation.position, player.transform.position) < activationRange)
            {
                openHandCursor = true;
                if (Input.GetMouseButtonDown(0) && inventory.GetSprite(itemRequired) != null)
                {
                    turnOn.SetActive(true);
                    inventory.Remove(itemRequired);
                }
                if (turnOn.activeSelf)
                {
                    video.Play();
                }
            }
        }

        //if (openHandCursor)
        //{
        //    Cursor.SetCursor(handCursor, Vector3.zero, CursorMode.Auto);
        //}
        //else
        //{

        //}
    }
}
