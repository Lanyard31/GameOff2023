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
    [SerializeField] ScrapCounter scrapCounter;
    [SerializeField] Image ammoBarImage;
    [SerializeField] Image ammoBarFillImage;
    [SerializeField] AudioSource upgradeSFX;
    [SerializeField] AudioSource upgradeCompleteSFX;
    [SerializeField] TextMeshProUGUI tierNumberText;
    [SerializeField] TextMeshProUGUI AmmoText;
    [SerializeField] Pause pause;

    [HideInInspector]
    public Weapon weapon; // The weapon to upgrade
    public bool canUpgrade;
    public GameObject upgradeBar; // The upgrade loading bar
    public GameObject particleEffectUpgrading; // The particle effect to display
    public Animator weaponAnimator; // The weapon's animator

    private int playerResources; // The player's resources
    private bool isUpgrading = false;
    private int upgradeCost;
    private int newTier;

    void Update()
    {
        if (pause.isPaused) return;
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("West"))
        {
            if (canUpgrade && !isUpgrading)
            {
                StartUpgrade();
            }
        }
        else if (isUpgrading == true && !Input.GetKey(KeyCode.Q) && !Input.GetButton("West"))
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
        HoldQText.gameObject.SetActive(false);
        GetCurrentWeapon();
        //Check if they have the required cost to enter the new Tier for the currently selected Weapon
        newTier = weapon.currentTierInt + 1;

        playerResources = scrapCounter.scrapCount;
        upgradeCost = weapon.weaponTiers.tiers[newTier].costToEnter;

        if (playerResources >= upgradeCost)
        {
            //Play an animation on the Weapon
            if (isUpgrading == false)
            {
                weaponAnimator = weapon.GetComponentInChildren<Animator>();
                weaponAnimator.Play("UpgradeAnim");
                isUpgrading = true;
            }

            upgradeBar.SetActive(true);
            weapon.canShoot = false;
            particleEffectUpgrading.SetActive(true);
            AmmoText.text = "Upgrading";
            StartCoroutine("FillUpgradeBar");
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
            //Allow cancelling
            StopAllCoroutines();
            weaponAnimator.SetTrigger("exitLoop");
            upgradeSFX.Stop();
            ammoBarImage.enabled = true;
            ammoBarFillImage.enabled = true;
            upgradeBar.GetComponentInChildren<Slider>().value = 0.0f;
            upgradeBar.SetActive(false);
            weapon.canShoot = true;
            particleEffectUpgrading.SetActive(false);
            isUpgrading = false;
            scrapCounter.UpdateScrapText(0);
            AmmoText.text = "Ammo";
            //denied SFX maybe
        }
    }

    void FinishUpgrade()
    {
        if (isUpgrading)
        {
            //If the loading bar successfully fills up, the public UpgradeWeapon is called on the currently active Weapon script
            weaponAnimator.SetTrigger("exitLoop");
            upgradeBar.SetActive(false);
            weapon.canShoot = true;
            particleEffectUpgrading.SetActive(false);
            //SFX for Upgrade Complete
            upgradeCompleteSFX.Play();
            isUpgrading = false;
            tierNumberText.text = (newTier + 1).ToString();
            weapon.UpgradeWeapon(newTier);
            scrapCounter.UpdateScrapText(-upgradeCost);
            AmmoText.text = "Ammo";
        }
    }

    IEnumerator FillUpgradeBar()
    {
        ammoBarImage.enabled = false;
        ammoBarFillImage.enabled = false;
        Slider slider = upgradeBar.GetComponentInChildren<Slider>();
        float timeToFill = weapon.weaponTiers.tiers[newTier].timeToUpgrade;
        float elapsedTime = 0.0f;

        while (elapsedTime < timeToFill)
        {
            //play looping sfx
            if (!upgradeSFX.isPlaying)
            {
                upgradeSFX.Play();
            }
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Lerp(0.0f, 1.0f, elapsedTime / timeToFill);
            yield return null;
        }
        ammoBarImage.enabled = true;
        ammoBarFillImage.enabled = true;
        upgradeSFX.Stop();
        FinishUpgrade();
    }
    
}
