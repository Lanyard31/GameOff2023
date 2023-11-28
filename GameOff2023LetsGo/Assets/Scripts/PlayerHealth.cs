using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] DamageEffect damageEffectScript;
    [SerializeField] HealthCounter healthCounter;
    [SerializeField] AudioSource hurtSFX;

    public void TakeDamage(float damage)
    {
        hitPoints = hitPoints - damage;
        damageEffectScript.ApplyDamageEffect();
        healthCounter.UpdateHealth(hitPoints);
        
        //randomize pitch
        float randomPitch = Random.Range(0.85f, 1.15f);
        //play as OneShot
        hurtSFX.PlayOneShot(hurtSFX.clip, randomPitch);

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
