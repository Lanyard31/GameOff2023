using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTiers", menuName = "ScriptableObjects/WeaponTiers", order = 1)]
public class WeaponTiers : ScriptableObject
{
    public WeaponTier[] tiers = new WeaponTier[5];
}