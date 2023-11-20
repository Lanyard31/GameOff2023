using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearGrab : MonoBehaviour
{
    public int gearCounter = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gear"))
        {
            // Disable the gear, add to the counter, and perform other actions
            //other.gameObject.SetActive(false);
            gearCounter++;
        }
    }
}
