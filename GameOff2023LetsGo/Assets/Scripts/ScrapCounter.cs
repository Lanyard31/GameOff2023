using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScrapCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scrapText;
    [SerializeField] Upgrader upgrader;
    [SerializeField] RawImage weaponIcon;
    [SerializeField] TextMeshProUGUI weaponName;
    [SerializeField] TextMeshProUGUI tierNumberText;
    Transform currentWeapon;
    public int scrapCount = 0;
    private int upgradeCost = 0;
    //savable?
    public Transform lastWeapon;

    public void Start()
    {
        UpdateUIInfo(lastWeapon);
        UpdateScrapText(0);
    }

    public void UpdateScrapText(int additionalScrap)
    {
        scrapCount += additionalScrap;
        upgradeCost = GetUpgradeCost();
        if (upgradeCost == 999)
        {
            return;
        }

        float scrapCountFloat = scrapCount / 10.0f;
        float upgradeCostFloat = upgradeCost / 10.0f;

        scrapText.text = scrapCountFloat.ToString() + " kg / " + upgradeCostFloat.ToString() + " kg";
        
        if (scrapCount >= upgradeCost)
        {
            upgrader.EnableUpgradeButton();
        }
        else
        {
            upgrader.DisableUpgradeButton();
        }
    }

    public void UpdateUIInfo(Transform weapon)
    {
        currentWeapon = weapon;
        UpdateScrapText(0);
        //access weapon icon via Weapon Tiers
        weaponIcon.texture = GetWeaponIcon();
        //access weapon name via Weapon Tiers
        weaponName.text = GetWeaponName();
        //update UI Icon
        tierNumberText.text = GetTierNumber().ToString();
    }

    private object GetTierNumber()
    {
        Weapon currentWeaponComponent = currentWeapon.GetComponent<Weapon>();
        WeaponTiers weaponTiers = currentWeaponComponent.weaponTiers;
        int currentTier = weaponTiers.currentTier;
        return currentTier + 1;
    }

    private string GetWeaponName()
    {
        Weapon currentWeaponComponent = currentWeapon.GetComponent<Weapon>();
        WeaponTiers weaponTiers = currentWeaponComponent.weaponTiers;
        return weaponTiers.GunName;
    }

    private int GetUpgradeCost()
    {
        Weapon currentWeaponComponent = currentWeapon.GetComponent<Weapon>();
        WeaponTiers weaponTiers = currentWeaponComponent.weaponTiers;
        int currentTier = weaponTiers.currentTier;

        // Check if this is the max tier
        if (currentTier >= weaponTiers.tiers.Length - 1)
        {
            // This is the max tier, initiate different behavior
            // For example, return a specific value
            SetMaxTierUI();
            return 999;
        }

        WeaponTier nexttier = weaponTiers.tiers[currentTier + 1];

        return nexttier.costToEnter;
    }

    private void SetMaxTierUI()
    {
        tierNumberText.text = "∞";
        scrapText.text = "MAX";
        upgrader.DisableUpgradeButton();
    }

    private Texture GetWeaponIcon()
    {
        Weapon currentWeaponComponent = currentWeapon.GetComponent<Weapon>();
        WeaponTiers weaponTiers = currentWeaponComponent.weaponTiers;

        return weaponTiers.iconTexture;
    }

}
