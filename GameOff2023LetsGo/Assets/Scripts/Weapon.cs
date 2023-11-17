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

    float shootTimer = 0.0f;
    float firingSpeed = 0.1f;

    bool canShootSingle = true;

    private void OnEnable()
    {
        canShootSingle = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (automaticFire && Input.GetMouseButton(0))
        {
            // Check if enough time has passed since the last shot
            if (Time.time - shootTimer >= firingSpeed)
            {
                ShootAuto();
                shootTimer = Time.time;
            }
        }

        else if (!automaticFire && canShootSingle && Input.GetMouseButton(0))
        {
            StartCoroutine(ShootOnce());
        }
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
            PlayMuzzleFlash();
            ProcessRaycast();
            PlayGunshotSound();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            weaponSwitcher.NextWeapon();
        }
    }

    IEnumerator ShootOnce()
    {
        canShootSingle = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            PlayGunshotSound();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            weaponSwitcher.NextWeapon();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShootSingle = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        // Define a layer mask excluding the player layer
        int playerLayer = LayerMask.NameToLayer("Player");
        int layerMask = 1 << playerLayer;
        layerMask = ~layerMask;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, layerMask))
        {
            CreateHitImpact(hit);

            // Check if the hit object is not on the player layer
            if (hit.transform.gameObject.layer != playerLayer)
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;

                target.TakeDamage(damage);
            }
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitVFX, hit.point, Quaternion.LookRotation(hit.normal));
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
}