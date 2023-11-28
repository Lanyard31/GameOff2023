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
    FirstPersonController firstPersonController;


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
        firstPersonController.enabled = false;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetHealthToZero()
    {
        healthSlider.value = 0;
    }
}
