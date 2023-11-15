using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Animator animator;
    private bool playerInRange = false;
    private bool doorsAreOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is in range
        if (playerInRange)
        {
            // Play the animation
            animator.Play("doorsAnim");
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.gameObject.tag == "Player" && doorsAreOpen == false)
        {
            // Set the player in range
            playerInRange = true;
            //enabled = false;
        }
    }

//Better to close doors and delete script with another invis trigger object if necessary

/*
    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is the player
        if (other.gameObject.tag == "Player" && doorsAreOpen == true)
        {
            // Set the player out of range
            playerInRange = false;

            enabled = false;
        }
    }
    */
    
}