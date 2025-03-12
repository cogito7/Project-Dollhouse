using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject pickupObject;
    private float rotationSpeed;
    void Start()
    {
        rotationSpeed = .001f;
        pickupObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Update()
    {
        pickupObject.transform.Rotate(1f * Time.deltaTime * rotationSpeed, 1f, 0f, Space.Self);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("item collected");
        if (collision.gameObject.name == "Player2")
        {
            Destroy(this.transform.gameObject);
            
        }
    }
}

