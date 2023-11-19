using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    // Adjust the self-destruct delay in seconds
    [SerializeField] float selfDestructDelay = 3f;

    void Start()
    {
        // Invoke the SelfDestruct method after the specified delay
        Invoke("SelfDestruction", selfDestructDelay);
    }

    void SelfDestruction()
    {
        // Destroy the GameObject to which this script is attached
        Destroy(gameObject);
    }
}
