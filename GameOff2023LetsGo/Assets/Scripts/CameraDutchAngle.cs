using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraDutchAngle : MonoBehaviour
    {
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] float rotationSpeed = 10f;
    private float turnInput;

    public void AdjustDutchBasedOnInput(Vector2 moveInput)
    {
        // Assuming moveInput.x represents the horizontal movement
        turnInput = moveInput.x;
    }

    void LateUpdate()
    {
        //float turnInput = Input.GetAxis("Horizontal");
        AdjustDutchAngle(turnInput);
    }

    void AdjustDutchAngle(float turnInput)
    {
        // Adjust Dutch angle based on player input
        float targetDutch = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.Dutch, -turnInput * 8f, Time.deltaTime * rotationSpeed);

        // Clamp the Dutch angle to a certain range if needed
        targetDutch = Mathf.Clamp(targetDutch, -1.25f, 1.25f);

        // Apply the modified Dutch angle back to the Cinemachine Virtual Camera
        cinemachineVirtualCamera.m_Lens.Dutch = targetDutch;
    }
}
