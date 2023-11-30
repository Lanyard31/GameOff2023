using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    [SerializeField] EnemyHealth EnemyHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] float lerpSpeed = 2f;
    [SerializeField] EndingSequence endingSequence;
    float maxHealth;

    void OnEnable()
    {
        healthSlider.value = 0.2f;
        maxHealth = EnemyHealth.GetHitPoints();
    }

    void Update()
    {
        if (EnemyHealth != null)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, EnemyHealth.GetHitPoints() / maxHealth, Time.deltaTime * lerpSpeed);
        }
        else
        {
            //disable the slider
            healthSlider.gameObject.SetActive(false);
            endingSequence.OnEnemyKilled();
        }
    }
}
