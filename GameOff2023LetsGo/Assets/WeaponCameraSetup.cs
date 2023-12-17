using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponCameraSetup : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera weaponCamera;

    void Start()
    {
        // Set up the layer mask for the weapon camera
        //weaponCamera.m_LayerMask = LayerMask.GetMask("Weapons");
    }
}
