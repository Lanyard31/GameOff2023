using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 10f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] bool automaticFire = false;
    [SerializeField] float timeBetweenShots = 0.5f;

    float shootTimer = 0.0f;
    float firingSpeed = 0.1f;

    bool canShootSingle = true;


    void Update()
    {
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

    void ShootAuto()
    {
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo();
        }
        else
        {
            //click noise
        }
    }

    IEnumerator ShootOnce()
    {
        canShootSingle = false;
        if (ammoSlot.GetCurrentAmmo() > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo();
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
