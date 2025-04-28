using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

public class CodeManager : MonoBehaviour
{
    public GameObject zoomedPaper;


    public GameObject[] players; // Assign both Player and Player2 in the Inspector
    public float pickupRange = 3f;
    public Vector2 cursorHotspot = Vector2.zero;
    public Texture2D handCursor;
    private bool isCursorOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoomedPaper.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            zoomedPaper.SetActive(true);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            zoomedPaper.SetActive(false);
        }





        bool openHandCursor = false;

        // Press left mouse to pick up if close to active player
        foreach (GameObject player in players)
        {
            if (player.activeInHierarchy && Vector3.Distance(transform.position, player.transform.position) < pickupRange)
            {
                openHandCursor = true;
                if (Input.GetMouseButtonDown(0))
                {
                    zoomedPaper.SetActive(true);
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
