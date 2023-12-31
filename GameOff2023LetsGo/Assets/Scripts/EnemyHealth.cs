using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    private float maxHealth;
    [SerializeField] GameObject[] explosions;
    [SerializeField] ObjectPool gearPool;
    [SerializeField] int gearsToAdd = 3;
    [SerializeField] HitFlash hitFlash;
    [SerializeField] GameObject fireParticle;

    private MeshFlash meshFlash;

    public bool isObject;
    public bool isDead = false;
    bool isBurning = false;

    private void Start()
    {
        maxHealth = hitPoints;
        if (gearPool == null)
        {
            FindObjectOfType<ObjectPool>();
        }
        if (isObject)
        {
            meshFlash = GetComponent<MeshFlash>();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints = hitPoints - damage;
        if (!isObject)
        {
            hitFlash.EnemyHitFlash();
        }
        else
        {
            meshFlash.EnemyHitFlash();
        }
        if (!fireParticle.activeSelf && hitPoints <= (0.20 * maxHealth) && hitPoints >= (0.01 * maxHealth))
        {
            Invoke("ActivateFire", 0.18f);
        }
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void ActivateFire()
    {
        if (isDead) return;
        if (isBurning) return;
        isBurning = true;
        fireParticle.SetActive(true);
        StartCoroutine(BurningEffect());
    }

    private void MeshHitFlash()
    {
        throw new NotImplementedException();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        if (!isObject)
        {
            GetComponent<Animator>().SetTrigger("die");
        }
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

    //a public getter fuunction for the hitPoints variable
    public float GetHitPoints()
    {
        return hitPoints;
    }

    private IEnumerator BurningEffect()
    {
        WaitForSeconds delay = new WaitForSeconds(1.2f);

        while (hitPoints > 0)
        {
            yield return delay;

            if (isDead)
            {
                yield break;
            }
            TakeDamage(1f);

        }
    }
}