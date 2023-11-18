using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float lowRangeThreshold = 5f;
    [SerializeField] float highRangeThreshold = 20f;
    [SerializeField] bool doorsCanClose;
    private Animator animator;
    private bool doorsHaveClosed;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (doorsHaveClosed) return;
        float playerRange = Vector3.Distance(transform.position, playerTransform.position);

        if (playerRange <= lowRangeThreshold)
        {
            OpenDoors();
        }

        else if (doorsCanClose && playerRange >= highRangeThreshold)
        {
            CloseDoors();
        }
    }

    private void OpenDoors()
    {
        animator.Play("doorsAnim");
    }

    private void CloseDoors()
    {
        animator.Play("doorsCloseAnim");
        doorsHaveClosed = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lowRangeThreshold);
        Gizmos.DrawWireSphere(transform.position, highRangeThreshold);
    }
}