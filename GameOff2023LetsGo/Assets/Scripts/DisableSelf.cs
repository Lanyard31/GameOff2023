using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    public float timeUntilDisable = 1f;
    private bool tickDownTimer = false;

    void OnEnable()
    {
        tickDownTimer = true;
    }

    void Update()
    {
        if (tickDownTimer)
        {
            timeUntilDisable -= Time.deltaTime;
            if (timeUntilDisable <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
}
