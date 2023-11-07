using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 1f;
    FirstPersonController fpsController;

    private void Awake()
    {
        //playerCamera.m_Lens.FieldOfView = zoomedOutFOV;
        fpsController = GetComponentInParent<FirstPersonController>();
        //Zoom(zoomedOutFOV);
        fpsController.RotationSpeed = zoomOutSensitivity;
    }

    private void Update()
    {
        CheckZoom();
    }

    private void CheckZoom()
    {
        if (Input.GetMouseButton(1) && playerCamera.m_Lens.FieldOfView != zoomedInFOV)
        {
            Zoom(zoomedInFOV);
            fpsController.RotationSpeed = zoomInSensitivity;
        }
        else if (!Input.GetMouseButton(1) && playerCamera.m_Lens.FieldOfView != zoomedOutFOV)
        {
            Zoom(zoomedOutFOV);
            fpsController.RotationSpeed = zoomOutSensitivity;
        }
    }

    private void Zoom(float targetFOV)
    {
        playerCamera.m_Lens.FieldOfView = Mathf.Lerp(playerCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
