using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public Weapon weapon; // The weapon to upgrade
    public int playerResources; // The player's resources
    public GameObject upgradeBar; // The upgrade loading bar
    public GameObject resourceDisplay; // The resource display
    public GameObject particleEffect; // The particle effect to display
    public Animator weaponAnimator; // The weapon's animator

    private WeaponSwitcher weaponSwitcher;
    private bool isUpgrading = false;
    private int upgradeCost;
    private int newTier;

    void Update()
    {
        // Step 1: Detect if the player is pressing 'Q' or East on a Controller
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("East"))
        {
            StartUpgrade();
        }
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetButtonUp("East"))
        {
            CancelUpgrade();
        }
    }

    void StartUpgrade()
    {
        GetCurrentWeapon();
        // Step 2: Check if they have the required cost to enter the new Tier for the currently selected Weapon
        newTier = weapon.currentTierInt + 1;

        if (playerResources >= upgradeCost)
        {
            // Step 3: Play an animation on the Weapon
            weaponAnimator.Play("Upgrade");

            // Step 4: Display an upgrade loading bar onscreen
            upgradeBar.SetActive(true);

            // Step 5: Display the resource for the cost to enter ticking down rapidly
            resourceDisplay.SetActive(true);

            // Step 6: Disable Shooting while this is occurring
            weapon.canShoot = false;

            // Step 8: Display a particle effect
            particleEffect.SetActive(true);

            isUpgrading = true;
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
            weaponAnimator.StopPlayback();
            upgradeBar.SetActive(false);
            resourceDisplay.SetActive(false);
            weapon.canShoot = true;
            particleEffect.SetActive(false);
            isUpgrading = false;
        }
    }

    void FinishUpgrade()
    {
        if (isUpgrading)
        {
            // Step 9: And if the loading bar successfully fills up, the public UpgradeWeapon is called on the currently active Weapon script
            weapon.UpgradeWeapon(newTier);
            upgradeBar.SetActive(false);
            resourceDisplay.SetActive(false);
            weapon.canShoot = true;
            particleEffect.SetActive(false);
            isUpgrading = false;
        }
    }
}