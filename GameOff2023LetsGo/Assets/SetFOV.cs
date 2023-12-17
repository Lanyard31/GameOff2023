using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFOV : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerCamera;
    [SerializeField] Slider fovSlider;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 50f;
    public float targetFOV;

    public void SetLevel(float sliderValue)
    {
        targetFOV = minFOV + (maxFOV - minFOV) * sliderValue;
        playerCamera.m_Lens.FieldOfView = targetFOV;
    }

    //guns must slide back
    // probably must deal with Laser HitVFX spawn
    //must be saved in PlayerPrefs

}
