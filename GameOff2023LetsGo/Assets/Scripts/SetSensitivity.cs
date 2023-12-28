using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class SetSensitivity : MonoBehaviour
{

    [SerializeField] FirstPersonController firstPersonController;
    [SerializeField] Slider sensitiveSlider;

    public void Start()
    {
        OnSensitivitySliderChanged(PlayerPrefs.GetFloat("TargetSensitiveSliderValue", 0.475f));
    }

    public void OnSensitivitySliderChanged(float sliderValue)
    {
        if (sensitiveSlider.value != sliderValue)
        {
            sensitiveSlider.value = sliderValue;
        }

        // Directly set the sensitivityMultiplier based on the slider value
        firstPersonController.sensitivityMultiplier = sliderValue * 2f + 0.05f; // Adjust the multiplier range as needed
        PlayerPrefs.SetFloat("TargetSensitiveSliderValue", sliderValue);
    }
}
