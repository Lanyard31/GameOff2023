using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] DamageEffect damageEffectScript;
    [SerializeField] HealthCounter healthCounter;
    [SerializeField] AudioSource hurtSFX;

    public void TakeDamage(float damage, Vector3 enemyPosition)
    {
        hitPoints = hitPoints - damage;
        damageEffectScript.ApplyDamageEffect();
        healthCounter.UpdateHealth(hitPoints);
        
        AudioSource.PlayClipAtPoint(hurtSFX.clip, enemyPosition);

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
