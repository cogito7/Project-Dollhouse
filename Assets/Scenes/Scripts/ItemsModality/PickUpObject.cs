using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public string itemID; // unique ID for pickup tracking
    public Sprite itemSprite;
    public GameObject[] players; // Assign both Player and Player2 in the Inspector
    public float pickupRange = 2f;
    public Vector2 cursorHotspot = Vector2.zero;
    public Texture2D handCursor;
    private float rotationSpeed = 0.001f;
    private bool isCollected = false;
    private bool isCursorOver = false;


    void Start()
    {

    }

    void Update()
    {
        // Rotate for visual feedback
        transform.Rotate(1f * Time.deltaTime * rotationSpeed, 1f, 0f, Space.Self);

        //tracks cursor
        bool openHandCursor = false;

        // Press left mouse to pick up if close to active player
        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy && Vector3.Distance(transform.position, player.transform.position) < pickupRange)
            {
                openHandCursor = true;
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Item collected by " + player.name);
                    Collect();
                    break;
                }
            }

        }
        //handle cursor swap
        if (openHandCursor && !isCursorOver)
        {
            Cursor.SetCursor(handCursor, cursorHotspot, CursorMode.Auto);
            isCursorOver = true;
        }
        else if (!openHandCursor && isCursorOver)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isCursorOver = false;
        }

    }

    public void Collect()
    {
        if (isCollected)
            return;

        Debug.Log("Item collected");




        ItemTracker.Instance.MarkItemAsPicked(itemID);

        // Update inventory and check for combination
        CraftingInventory.Instance.AddItemToInventory(itemID, itemSprite);

        // Reset cursor before hiding the object
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        isCollected = true;

        // Hide the object in the scene
        gameObject.SetActive(false);
    }

    public bool IsCollected()
    {
        return isCollected;
    }
}
