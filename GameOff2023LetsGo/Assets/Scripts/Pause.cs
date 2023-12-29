using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    
    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] Canvas pauseCanvas;
    //is read by the Weapons
    public bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Start"))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        firstPersonController.isPaused = isPaused;
        pauseCanvas.enabled = isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
