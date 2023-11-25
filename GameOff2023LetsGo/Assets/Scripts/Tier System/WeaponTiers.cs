using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTiers", menuName = "ScriptableObjects/WeaponTiers", order = 1)]
public class WeaponTiers : ScriptableObject
{
    public Texture iconTexture;
    public string GunName;
    public WeaponTier[] tiers = new WeaponTier[5];
    public int currentTier = 0;
}