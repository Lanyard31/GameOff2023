using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] bool automaticFire = false;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] Slider ammoSlider;
    [SerializeField] AudioClip[] gunshotSounds;
    [SerializeField] AudioSource gunshotAudioSource;
    [SerializeField] float volumeLowEnd = 0.3f;
    [SerializeField] float volumeHighEnd = 0.4f;
    [SerializeField] WeaponSwitcher weaponSwitcher;
    [SerializeField] WeaponRecoil weaponRecoil;
    [SerializeField] float recoilTimer;
    [SerializeField] float recoilAmount = 0.1f;
    [SerializeField] GameObject laserTracer;
    [SerializeField] AudioSource weaponIsEmptySFX;
    [SerializeField] Material gunMaterialColor;

    public WeaponTiers weaponTiers;
    public int currentTierInt;
    public bool canShoot = true;
    public bool laser = false;

    float shootTimer = 0.0f;
    float firingSpeed = 0.1f;
    float originalDamage;
    float originalFiringSpeed;
    bool canShootSingle = true;

    private void Start()
    {
        //load the data
        originalDamage = damage;
        originalFiringSpeed = firingSpeed;
        currentTierInt = weaponTiers.currentTier;
        UpgradeWeapon(currentTierInt);
    }

    private void OnEnable()
    {
        canShootSingle = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (!canShoot) return;
        if (automaticFire && Input.GetMouseButton(0))
        {
            CaptureMouse();
            //activate laser
            if (laser == true)
            {
                laserTracer.SetActive(true);
                laserTracer.GetComponent<DisableSelf>().timeUntilDisable = 0.2f;
            }
            // Check if enough time has passed since the last shot
            if (Time.time - shootTimer >= firingSpeed)
            {
                ShootAuto();
                shootTimer = Time.time;
            }
        }

        else if (!automaticFire && canShootSingle && Input.GetMouseButton(0))
        {
            CaptureMouse();
            StartCoroutine(ShootOnce());
        }


    }

    private void CaptureMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisplayAmmo()
    {
        if (ammoSlider != null)
        {
            float ammoRatio = (float)ammoSlot.GetCurrentAmmo(ammoType) / (float)ammoSlot.GetMaxAmmo(ammoType);
            ammoSlider.value = ammoRatio;
        }
    }

    void ShootAuto()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            Recoil();
            PlayMuzzleFlash();
            ProcessRaycast();
            PlayGunshotSound();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            weaponIsEmptySFX.Play();
            ammoSlot.SetCurrentAmmo(ammoType, (ammoSlot.GetMaxAmmo(ammoType)));
            weaponSwitcher.NextWeapon();
        }
    }

    IEnumerator ShootOnce()
    {
        canShootSingle = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            Recoil();
            PlayMuzzleFlash();
            ProcessRaycast();
            PlayGunshotSound();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            weaponIsEmptySFX.Play();
            ammoSlot.SetCurrentAmmo(ammoType, (ammoSlot.GetMaxAmmo(ammoType)));
            weaponSwitcher.NextWeapon();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShootSingle = true;
    }

    private void Recoil()
    {
        weaponRecoil.ApplyRecoil(recoilTimer, recoilAmount);
    }

    private void PlayMuzzleFlash()
    {
        if (!muzzleFlash.isPlaying)
        {
            muzzleFlash.Play();
        }
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        // Define layers
        int playerLayer = LayerMask.NameToLayer("Player");
        int obstacleLayer = LayerMask.NameToLayer("Gear");

        // Create layer masks
        int playerLayerMask = 1 << playerLayer;
        int obstacleLayerMask = 1 << obstacleLayer;

        // Combine layer masks using bitwise OR
        int combinedLayerMask = playerLayerMask | obstacleLayerMask;

        // Invert the combined layer mask to exclude both Player and Obstacle layers
        int finalLayerMask = ~combinedLayerMask;

        // Perform the raycast with the final layer mask
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, finalLayerMask))
        {
            CreateHitImpact(hit);

            // Check if the hit object is not on the player layer
            if (hit.transform.gameObject.layer != playerLayer)
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                if (target.isDead == true) return;

                float randomizedDamage = damage + UnityEngine.Random.Range(-5, 5);
                target.TakeDamage(randomizedDamage);
                Vector3 skewedHitPoint = new Vector3(hit.point.x + UnityEngine.Random.Range(-0.3f, 0.3f), hit.point.y + 0.25f, hit.point.z + UnityEngine.Random.Range(-0.3f, 0.3f));
                DynamicTextManager.CreateText(skewedHitPoint, randomizedDamage.ToString(), DynamicTextManager.defaultData);
            }
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        float offset = 0.05f;
        Vector3 impactPosition = hit.point + hit.normal * offset;
        GameObject impact = Instantiate(hitVFX, impactPosition, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }

    public void PlayGunshotSound()
    {
        if (gunshotSounds.Length == 0 || gunshotAudioSource == null)
        {
            Debug.LogError("Gunshot sounds or audio source not set!");
            return;
        }

        // Randomly select a gunshot sound from the array
        AudioClip selectedSound = gunshotSounds[UnityEngine.Random.Range(0, gunshotSounds.Length)];

        // Set random variations for pitch and volume
        float pitch = UnityEngine.Random.Range(0.9f, 1.1f); // Adjust the range as needed
        float volume = UnityEngine.Random.Range(volumeLowEnd, volumeHighEnd); // Adjust the range as needed

        // Play the gunshot sound with variations
        gunshotAudioSource.pitch = pitch;
        gunshotAudioSource.volume = volume;
        gunshotAudioSource.PlayOneShot(selectedSound);
    }

    public void UpgradeWeapon(int newTier)
    {
        WeaponTier currentTier = weaponTiers.tiers[currentTierInt];
        currentTierInt = newTier;
        currentTier = weaponTiers.tiers[newTier];
        //add new tier to weaponTiers
        weaponTiers.currentTier = newTier;
        damage = originalDamage + currentTier.damageBonus;
        firingSpeed = originalFiringSpeed + currentTier.firingSpeedBonus;
        transform.localScale = currentTier.gunMeshScale;
        gunMaterialColor = currentTier.gunMaterial;
        //sets ammo to new max Ammo
        ammoSlot.SetMaxAmmo(ammoType, (ammoSlot.GetMaxAmmo(ammoType) + currentTier.maxAmmoBonus));
        ammoSlot.SetCurrentAmmo(ammoType, (ammoSlot.GetMaxAmmo(ammoType)));

        //tracer effect?
        //special effect?
    }
}