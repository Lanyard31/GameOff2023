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
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            //click noise
        }
    }

    IEnumerator ShootOnce()
    {
        canShootSingle = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        else
        {
            //click noise
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
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;

            target.TakeDamage(damage);
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
}
