using UnityEngine;

public class DollhouseInteraction : MonoBehaviour
{
    public GameObject player; // Assign in Inspector
    public GameObject player2; // Assign in Inspector
    public GameObject PickUpObject;
    public float pickupRange = 2f;


    // Updates once per frame
    void Update()
    {
        // Check for mouse click to interact with Dollhouse
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < PickUpObject.transform.childCount; i++)
            {
                GameObject item = PickUpObject.transform.GetChild(i).gameObject;
                float dist = Vector3.Distance(player.transform.position, item.transform.position);
                if (dist < pickupRange) // TODO: Replace 1 with an actual distance 
                {
                    PickUpObject pickupScript = item.GetComponent<PickUpObject>();

                        Debug.Log("Picking up item: " + item.name); // Debugging collection
                        pickupScript.Collect(); // Call the collect method in the item script
                        

                }
            }
        }
    }

}
