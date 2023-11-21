using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] GameObject[] explosions;
    [SerializeField] ObjectPool gearPool;
    [SerializeField] int gearsToAdd = 3;

    public bool isDead = false;

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
        Invoke("Kaboom", UnityEngine.Random.Range(0.6f, 0.85f));
    }

    public void Kaboom()
    {
        int randomIndex = UnityEngine.Random.Range(0, explosions.Length);
        SpawnGears();
        Instantiate(explosions[randomIndex], transform.position, explosions[randomIndex].transform.rotation);
        Destroy(gameObject);
    }

    void SpawnGears()
    {
        for (int i = 0; i < gearsToAdd; i++)
        {
            GameObject gear = gearPool.GetPooledObject();
            gear.SetActive(true);
            gear.transform.position = transform.position; // Adjust position as needed
        }
    }
}