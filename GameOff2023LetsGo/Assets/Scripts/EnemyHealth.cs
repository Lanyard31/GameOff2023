using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] GameObject[] explosions;

    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints = hitPoints - damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        //Destroy(gameObject);
        Invoke("Kaboom", 1.2f);
    }

    public void Kaboom()
    {
        int randomIndex = UnityEngine.Random.Range(0, explosions.Length);
        Instantiate(explosions[randomIndex], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}