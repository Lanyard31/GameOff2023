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
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        firstPersonController.isPaused = isPaused;
        pauseCanvas.enabled = isPaused;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
