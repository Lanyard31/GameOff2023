using UnityEngine;

[System.Serializable]
public class WeaponTier
{
    public int costToEnter;
    public float damageBonus;
    public float firingSpeedBonus;
    public Vector3 gunMeshScale;
    public Material gunMaterial;
    public GameObject tracerParticleEffect;
    public GameObject specialEffect;
    // Add more parameters here as needed

    //string for weapon name
    //string for tier name
    //maxAmmo increase
    //scale for ammoBar
    //time required to upgrade

}