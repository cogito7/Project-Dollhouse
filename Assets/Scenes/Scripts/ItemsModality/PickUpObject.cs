using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject pickupObject;
    private float rotationSpeed;
    public string itemID; //unique ID for pickup tracking
    public Sprite itemSprite;

    void Start()
    {
        rotationSpeed = .001f;
        //pickupObject.GetComponent<Renderer>().material.color = Color.yellow;

        
    }

    void Update()
    {
        pickupObject.transform.Rotate(1f * Time.deltaTime * rotationSpeed, 1f, 0f, Space.Self);
    }
    void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.name == "Player2")
        {
            Debug.Log("item collected");
            ItemTracker.Instance.MarkItemAsPicked(itemID);//track item                                             
            CraftingInventory.Instance.AddItemToInventory(itemID, itemSprite);// Add to crafting panel slot
            Destroy(gameObject);
        }
    }
}

