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
    private bool isCursorOver = false;

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
                if (Input.GetMouseButtonDown(0) && turnOn.activeSelf && video)
                {
                    video.Play();
                }
            }
        }

        //handle cursor swap
        if (openHandCursor && !isCursorOver)
        {
            Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
            isCursorOver = true;
        }
        else if (!openHandCursor && isCursorOver)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isCursorOver = false;
        }
    }
}
