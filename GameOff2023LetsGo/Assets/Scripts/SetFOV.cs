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
    [SerializeField] GameObject weaponsPivot;
    public float targetFOV;
    [SerializeField] float pivotScale = 1.5f;
    [SerializeField] float pivotOffset = 1f;

    public void Start()
    {
        SetLevel(PlayerPrefs.GetFloat("TargetFOVSliderValue", 0.4f));
    }

    public void SetLevel(float sliderValue)
    {
        if (fovSlider.value != sliderValue)
        {
            fovSlider.value = sliderValue;
        }
        targetFOV = minFOV + (maxFOV - minFOV) * sliderValue;
        playerCamera.m_Lens.FieldOfView = targetFOV;

        // Calculate normalized FOV value
        float normalizedFOV = (targetFOV - minFOV) / (maxFOV - minFOV);

        // Adjust weaponPivot position based on normalized FOV
        float pivotPosition = -(normalizedFOV * pivotScale - pivotOffset); // Adjust the range based on your needs
        weaponsPivot.transform.localPosition = new Vector3(weaponsPivot.transform.localPosition.x, weaponsPivot.transform.localPosition.y, pivotPosition);

        // Save the targetFOV in PlayerPrefs or any other storage mechanism if needed
        PlayerPrefs.SetFloat("TargetFOVSliderValue", sliderValue);
    }

    

    //guns must slide back
    // probably must deal with Laser HitVFX spawn
    //must be saved in PlayerPrefs

}
