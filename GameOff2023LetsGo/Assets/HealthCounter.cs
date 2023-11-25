using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    public void UpdateHealth(float hitPoints)
    {
        StartCoroutine(UpdateHealthSlider(hitPoints));
    }

    private IEnumerator UpdateHealthSlider(float hitPoints)
    {
        // Normalize hitPoints to the range of the slider
        float normalizedHitPoints = hitPoints / 100.0f;

        // Lerps the Slider value to the new value
        float t = 0.0f;
        float currentHealth = healthSlider.value;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            healthSlider.value = Mathf.Lerp(currentHealth, normalizedHitPoints, t);
            yield return null;
        }
    }
}
