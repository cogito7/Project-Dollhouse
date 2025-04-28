using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

public class LockUI : MonoBehaviour
{
    public string correctCode = "7423";
    public GameObject doorToOpen;
    public GameObject lockPanel;
    public GameObject pianoKey;
    public Text playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lockPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            lockPanel.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            lockPanel.SetActive(false);
        }

        string inputText = playerInput.text;

        if(inputText.Equals(correctCode))
        {
            SubmitCode();
        }
    }

    public void SubmitCode()
    {
        doorToOpen.GetComponent<Animator>().SetBool("isOpen", true);
        pianoKey.SetActive(true);
        lockPanel.SetActive(false);
    }
}
