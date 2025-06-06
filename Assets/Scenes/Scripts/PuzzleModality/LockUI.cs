using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

public class LockUI : MonoBehaviour
{
    public string correctCode = "7423";
    //public GameObject doorToOpen;
    public GameObject lockPanel;
    public GameObject pianoKey;
    public Text playerInput;

    public GameObject[] players; // Assign both Player and Player2 in the Inspector
    public float pickupRange = 2f;
    public Vector2 cursorHotspot = Vector2.zero;
    public Texture2D handCursor;
    private bool isCursorOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockPanel.SetActive(false);
        pianoKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            lockPanel.SetActive(true);
        }*/
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            lockPanel.SetActive(false);
        }

        string inputText = playerInput.text;

        if(inputText.Equals(correctCode))
        {
            SubmitCode();
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
                    lockPanel.SetActive(true);
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

    public void SubmitCode()
    {
        //doorToOpen.GetComponent<Animator>().SetBool("isOpen", true);
        pianoKey.SetActive(true);
        lockPanel.SetActive(false);
    }
}
