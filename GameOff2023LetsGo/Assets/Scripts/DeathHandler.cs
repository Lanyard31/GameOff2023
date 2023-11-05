using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    FirstPersonController firstPersonController;

    private void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        gameOverCanvas.SetActive(false);
    }

    public void HandleDeath()
    {
        gameOverCanvas.SetActive(true);
        firstPersonController.enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
