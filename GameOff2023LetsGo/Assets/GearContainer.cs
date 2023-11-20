using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearContainer : MonoBehaviour
{

    private float bounceForce = 1.5f;
    private bool oneBounce = false;
    private bool canBounce = false;
    [SerializeField] Rigidbody rb;
    [SerializeField] SphereCollider sphereCollider;

    private void OnEnable()
    {
        // Reset any properties if needed when the gear is reused from the pool
        rb.isKinematic = false;
        sphereCollider.enabled = true;
        canBounce = false;
        Invoke("CanBounceActivator", 0.5f);
    }

    private void CanBounceActivator()
    {
        canBounce = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canBounce == true && oneBounce == false)
        {
            // Bounce the gear when it collides with the floor
            oneBounce = true;
            canBounce = false;
            BounceGear();
        }
    }

    private void BounceGear()
    {
        // Calculate a new upward force for bouncing
        Vector3 bounceForceDirection = Vector3.up + new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
        rb.AddForce(bounceForceDirection * bounceForce, ForceMode.Impulse);

        StartCoroutine(MakeKinematicAfterBounce());
    }

    private IEnumerator MakeKinematicAfterBounce()
    {
        // Wait for a short delay to allow the bounce animation to complete
        yield return new WaitForSeconds(2f);

        // Make the Rigidbody kinematic to prevent further physics interactions
        rb.isKinematic = true;
        sphereCollider.enabled = false;
    }
}
