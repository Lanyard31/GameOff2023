using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] Ammo ammoSlot;

    [SerializeField] Transform rotationPivot; // pivot point around which the gun will rotate
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    private bool gunRotating = false;
    private float pitchRange = 0.08f;

    void Start()
    {
        SetWeaponActive();
    }

    public void NextWeapon()
    {
        SetWeaponInactive();
        PlayReloadSound();
    }

    private void PlayReloadSound()
    {
        int randomIndex = UnityEngine.Random.Range(0, audioClips.Length);
        AudioClip selectedClip = audioClips[randomIndex];
        float pitch = UnityEngine.Random.Range(1f - pitchRange, 1f + pitchRange);
        audioSource.pitch = pitch;
        AudioSource.PlayClipAtPoint(selectedClip, transform.position);
    }

    private void SetWeaponActive()
    {
        Transform weapon = transform.GetChild(currentWeapon);
        StartCoroutine(RotateWeaponOnScreen(weapon));
    }

    private void SetWeaponInactive()
    {
        Transform weapon = transform.GetChild(currentWeapon);
        StartCoroutine(RotateWeaponOffScreen(weapon));
    }

    private IEnumerator RotateWeaponOnScreen(Transform weapon)
    {
        if (!gunRotating)
        {
            gunRotating = true;
            weapon.gameObject.SetActive(true);
            float t = 0.0f;
            Quaternion startRotation = Quaternion.Euler(0, 0, 90);
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

            while (t < 1.0f)
            {
                t += Time.deltaTime * rotationSpeed;
                rotationPivot.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
                yield return null;
            }
            //reset pivot rotation and weapon rotation
            rotationPivot.localRotation = Quaternion.Euler(0, 0, 0);
            weapon.localRotation = Quaternion.Euler(0, 270, 0);
            gunRotating = false;
        }
    }

    private IEnumerator RotateWeaponOffScreen(Transform weapon)
    {
        if (!gunRotating)
        {
            gunRotating = true;
            float t = 0.0f;
            Quaternion startRotation = rotationPivot.localRotation;
            Quaternion targetRotation = Quaternion.Euler(0, 0, -90);

            while (t < 1.0f)
            {
                t += Time.deltaTime * rotationSpeed;
                rotationPivot.localRotation = Quaternion.Slerp(startRotation, targetRotation, t);
                yield return null;
            }

            weapon.gameObject.SetActive(false);
            gunRotating = false;
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
            SetWeaponActive(); // Call SetWeaponActive here to start the next weapon rotation
        }
    }


    /*
    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 3;
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }
    */
}
