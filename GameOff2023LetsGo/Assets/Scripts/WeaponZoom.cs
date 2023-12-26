using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCamera;
    public float zoomedOutFOV = 60f;
    [SerializeField, Range(0f, 1f)] float zoomInPercentage = 0.5f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 1f;
    [SerializeField] SetFOV setFOV;
    FirstPersonController fpsController;

    private void Awake()
    {
        fpsController = GetComponentInParent<FirstPersonController>();
        fpsController.RotationSpeed = zoomOutSensitivity;
    }

    private void OnEnable()
    {
        zoomedOutFOV = setFOV.targetFOV;
    }

    private void Update()
    {
        CheckZoom();
    }

    private void CheckZoom()
    {
        zoomedOutFOV = setFOV.targetFOV;
        zoomedInFOV = zoomedOutFOV * (1f - zoomInPercentage);
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
