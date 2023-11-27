using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    //Array for all Scriptable Objects
    public WeaponTiers[] weaponTiers;
    public bool initialSave = false;

    void Awake()
    {
        //this is going to have to be activated on a seperate scene at the start of the game
        if (initialSave)
        {
            SaveTierDataInitial();
        }
        LoadTierData();
    }

    private void SaveTierDataInitial()
    {
        for (int i = 0; i < weaponTiers.Length; i++)
        {
            PlayerPrefs.SetInt("WeaponTier" + i, 0);
        }
    }

    public void SaveTierData()
    {
        for (int i = 0; i < weaponTiers.Length; i++)
        {
            PlayerPrefs.SetInt("WeaponTier" + i, weaponTiers[i].currentTier);
        }
    }

    public void LoadTierData()
    {
        for (int i = 0; i < weaponTiers.Length; i++)
        {
            weaponTiers[i].currentTier = PlayerPrefs.GetInt("WeaponTier" + i);
        }
    }
}
