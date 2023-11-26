using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HoldQText;
    [SerializeField] WeaponSwitcher weaponSwitcher;
    public bool canUpgrade;

    public Weapon weapon; // The weapon to upgrade
    public int playerResources; // The player's resources
    public GameObject upgradeBar; // The upgrade loading bar
    public GameObject resourceDisplay; // The resource display
    public GameObject particleEffect; // The particle effect to display
    public Animator weaponAnimator; // The weapon's animator

    
    private bool isUpgrading = false;
    private int upgradeCost;
    private int newTier;

    void Update()
    {
        // Step 1: Detect if the player is pressing 'Q' or the 2 button any gamepad 
        if (Input.GetKeyDown(KeyCode.Q) || (Input.GetButtonDown("West")))
        {
            if (canUpgrade)
            {
                StartUpgrade();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Q) || (Input.GetButtonUp("West")))
        {
            CancelUpgrade();
        }
    }

    public void EnableUpgradeButton()
    {
        canUpgrade = true;
        HoldQText.gameObject.SetActive(true);
    }

    public void DisableUpgradeButton()
    {
        canUpgrade = false;
        HoldQText.gameObject.SetActive(false);
    }

    void StartUpgrade()
    {
        GetCurrentWeapon();
        //Debug.Log(weapon.name);
        // Step 2: Check if they have the required cost to enter the new Tier for the currently selected Weapon
        newTier = weapon.currentTierInt + 1;


        if (playerResources >= upgradeCost)
        {
            // Step 3: Play an animation on the Weapon
            //get the WeaponAnimator from the currently active Weapon
            if (isUpgrading == false)
            {
                weaponAnimator = weapon.GetComponentInChildren<Animator>();
                weaponAnimator.Play("UpgradeAnim");
                isUpgrading = true;
            }

            // Step 4: Display an upgrade loading bar onscreen
            //upgradeBar.SetActive(true);

            // Step 5: Countdown the resource for the cost to enter ticking down rapidly
            //resourceDisplay.SetActive(true);

            // Step 6: Disable Shooting while this is occurring
            weapon.canShoot = false;

            // Step 8: Display a particle effect
            //particleEffect.SetActive(true);
        }
    }

    private void GetCurrentWeapon()
    {
        weapon = weaponSwitcher.transform.GetChild(weaponSwitcher.currentWeapon).GetComponent<Weapon>();
    }

    void CancelUpgrade()
    {
        if (isUpgrading)
        {
            // Step 7: Allow cancelling
            weaponAnimator.SetTrigger("exitLoop");
            //upgradeBar.SetActive(false);
            //resourceDisplay.SetActive(false);
            weapon.canShoot = true;
            //particleEffect.SetActive(false);
            isUpgrading = false;
        }
    }

    void FinishUpgrade()
    {
        if (isUpgrading)
        {
            // Step 9: And if the loading bar successfully fills up, the public UpgradeWeapon is called on the currently active Weapon script
            weaponAnimator.SetTrigger("exitLoop");
            //weapon.UpgradeWeapon(newTier);
            //upgradeBar.SetActive(false);
            //resourceDisplay.SetActive(false);
            weapon.canShoot = true;
            //particleEffect.SetActive(false);
            isUpgrading = false;
        }
    }
}
