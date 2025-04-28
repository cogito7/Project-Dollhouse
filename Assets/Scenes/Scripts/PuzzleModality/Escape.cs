using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject[] players; // Assign both Player and Player2 in the Inspector
    public GameObject pianoFixed;
    public float pickupRange = 2f;
    public Vector2 cursorHotspot = Vector2.zero;
    public Texture2D handCursor;
    private bool isCursorOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool openHandCursor = false;

        // Press left mouse to pick up if close to active player
        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy && Vector3.Distance(transform.position, player.transform.position) < pickupRange && pianoFixed.activeInHierarchy)
            {
                openHandCursor = true;
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene(2);
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
}
