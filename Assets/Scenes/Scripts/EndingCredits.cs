using UnityEngine;
using System.Collections;

public class EndingCredits : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(8f); // Wait for 10 seconds
        Application.LoadLevel(0); // Load the first scene (menu)
    }
}
