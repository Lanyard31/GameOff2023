using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject weaponsHiearchyParent;
    [SerializeField] HealthCounter healthCounter;
    [SerializeField] AudioSource audioSource;
    FirstPersonController firstPersonController;
    bool isDead = false;

    private void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        gameOverCanvas.SetActive(false);
    }

    public void HandleDeath()
    {
        //get the all weapons active in the hierarchy and disable the canShoot bool on all of them
        Weapon[] weapons = weaponsHiearchyParent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            weapon.GetComponent<Weapon>().canShoot = false;
        }
        healthCounter.StopAllCoroutines();
        SetHealthToZero();
        gameOverCanvas.SetActive(true);
        audioSource.Play();
        firstPersonController.enabled = false;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isDead = true;
    }

    private void SetHealthToZero()
    {
        healthSlider.value = 0;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (isDead && hasFocus)
        {
            // Lock and hide the cursor when the game regains focus
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
