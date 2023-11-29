using UnityEngine;

public class ExplodingBarrel : MonoBehaviour
{
    public float maxHealth = 100f;
    public float explosionRadius = 5f;
    public float explosionDamage = 50f;
    public GameObject explosionPrefab;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Detect and damage nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Apply damage to player
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(explosionDamage);
                }
            }
            else if (collider.CompareTag("Enemy"))
            {
                // Apply damage to enemy
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(explosionDamage);
                }
            }
            else if (collider.CompareTag("ExplodingBarrel"))
            {
                // Apply damage to another exploding barrel if needed
                ExplodingBarrel barrel = collider.GetComponent<ExplodingBarrel>();
                if (barrel != null)
                {
                    barrel.TakeDamage(explosionDamage);
                }
            }
        }

        // Destroy the exploding barrel
        Destroy(gameObject);
    }
}