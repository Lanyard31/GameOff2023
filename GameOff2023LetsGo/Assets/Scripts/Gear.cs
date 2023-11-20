using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    private float rotationSpeed;
    [SerializeField] Rigidbody rb;
    private float initialForce = 4f;

    [SerializeField] Animator animator;
    private Transform player;
    bool homingGearActivated = false;
    public float movementSpeed = 10f;
    private bool collected = false;


    private void OnEnable()
    {
        // Reset any properties if needed when the gear is reused from the pool
        InitializeGear();
    }

    private void InitializeGear()
    {
        animator.enabled = true;
        homingGearActivated = false;
        collected = false;
        // Apply an initial force in a random direction on the x/z plane
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
        rb.AddForce(randomDirection * initialForce, ForceMode.Impulse);

        // Generate a random rotation speed between 30 and 120 degrees per second
        rotationSpeed = Random.Range(30f, 35f);

        // Set a random initial rotation
        transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 359f));
    }

    private void Update()
    {
        // Add spinning behavior
        SpinGear();
        Homing();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the player
        if (other.gameObject.CompareTag("Player") && collected == false)
        {
            player = other.gameObject.transform;
            collected = true;
            homingGearActivated = true;
        }
    }

    private void Homing()
    {
        if (player != null && homingGearActivated == true)
        {
            animator.enabled = false;
            // Calculate the direction towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move towards the player with a smooth but quick speed
            transform.position += direction * movementSpeed * Time.deltaTime;

            // Check if the object is close enough to the player to be considered "there"
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer < 1f)
            {
                homingGearActivated = false;
                gameObject.SetActive(false);
                //disable parent?
                //SFX
            }
        }
    }



    private void SpinGear()
    {
        // Continuously increase the rotation based on the rotation speed
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Reset rotation if it exceeds 360 to avoid floating point errors
        if (transform.rotation.eulerAngles.z >= 360f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
