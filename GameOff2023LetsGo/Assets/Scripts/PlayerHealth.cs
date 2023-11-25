using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] DamageEffect damageEffectScript;
    [SerializeField] HealthCounter healthCounter;

    public void TakeDamage(float damage)
    {
        hitPoints = hitPoints - damage;
        damageEffectScript.ApplyDamageEffect();
        healthCounter.UpdateHealth(hitPoints);

        if (hitPoints <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
