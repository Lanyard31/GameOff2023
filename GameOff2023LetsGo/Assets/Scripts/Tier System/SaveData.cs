using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    //Array for all Scriptable Objects
    public WeaponTiers[] weaponTiers;
    //scrapCount
    public ScrapCounter scrapCounter;
    public bool initialSave = false;
    public WeaponSwitcher weaponSwitcher;

    void Start()
    {
        //this is going to have to be activated on a seperate scene at the start of the game
        if (initialSave)
        {
            SaveTierDataInitial();
            SaveScrapCountInitial();
        }
        LoadTierData();
        //check if null
        if (scrapCounter != null)
        {
            LoadScrapCount();
        }
    }

    private void SaveTierDataInitial()
    {
        for (int i = 0; i < weaponTiers.Length; i++)
        {
            PlayerPrefs.SetInt("WeaponTier" + i, 0);
        }
        PlayerPrefs.SetInt("LastWeapon", 0);
    }

    private void SaveScrapCountInitial()
    {
        PlayerPrefs.SetInt("ScrapCount", 0);
    }

    public void SaveScrapCount()
    {
        PlayerPrefs.SetInt("ScrapCount", scrapCounter.scrapCount);
    }

    public void SaveTierData()
    {
        for (int i = 0; i < weaponTiers.Length; i++)
        {
            PlayerPrefs.SetInt("WeaponTier" + i, weaponTiers[i].currentTier);
        }
    }

    public void SaveCurrentWeapon()
    {
        PlayerPrefs.SetInt("LastWeapon", weaponSwitcher.currentWeapon);
    }

    public void LoadScrapCount()
    {
        scrapCounter.scrapCount = PlayerPrefs.GetInt("ScrapCount");
    }

    public void LoadTierData()
    {
        for (int i = 0; i < weaponTiers.Length; i++)
        {
            weaponTiers[i].currentTier = PlayerPrefs.GetInt("WeaponTier" + i);
        }
    }
}
