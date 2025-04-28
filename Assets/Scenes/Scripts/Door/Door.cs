using UnityEngine;


public class Door : MonoBehaviour
{
    public float interactDistance = 5f;         // Distance to interact
    public string animationTrigger = "Open";    // Animator trigger name
    public string playerName = "player2";       // Name of the player GameObject

    private Animator animator;
    private Transform player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find(playerName).transform;  // Automatically find the player by name
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            if (player == null)
            {
                Debug.LogWarning("Player not found by name in DoorInteraction script.");
                return;
            }

            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= interactDistance)
            {
                animator.SetTrigger(animationTrigger);  // Triggers the "Open" animation
            }
        }
    }
}