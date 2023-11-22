using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponUpgradeManager", menuName = "Custom/WeaponUpgradeManager")]
public class WeaponUpgradeManager : ScriptableObject
{
    public WeaponUpgradeData[] weaponUpgradeDatas;

    public WeaponUpgradeData GetWeaponUpgradeData(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weaponUpgradeDatas.Length)
        {
            return weaponUpgradeDatas[weaponIndex];
        }
        else
        {
            Debug.LogError($"Weapon upgrade data not found for index {weaponIndex}.");
            return null;
        }
    }
}
