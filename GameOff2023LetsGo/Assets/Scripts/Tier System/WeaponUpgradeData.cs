using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponUpgradeData", menuName = "Custom/WeaponUpgradeData")]
public class WeaponUpgradeData : ScriptableObject
{
    public WeaponTier maxTier = WeaponTier.Tier5; // Set the maximum tier

    [SerializeField]
    public WeaponUpgradeParameters[] upgradeParameters;

    public WeaponUpgradeParameters GetUpgradeParameters(WeaponTier tier)
    {
        if (tier <= maxTier)
        {
            return upgradeParameters[(int)tier - 1]; // Adjust index to match enum values
        }
        else
        {
            Debug.LogError($"Upgrade parameters not found for {tier}.");
            return null;
        }
    }
}
