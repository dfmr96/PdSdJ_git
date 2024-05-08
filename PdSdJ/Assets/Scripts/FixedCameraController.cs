using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class FixedCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private PlayerDetector playerDetector;
    
    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        playerDetector.OnPlayerDetected += TurnCamera;
    }

    private void OnDisable()
    {
        playerDetector.OnPlayerDetected -= TurnCamera;
    }

    private void TurnCamera(CharacterController characterController)
    {
        bool turnOn = characterController != null;
        cam.enabled = turnOn;
    }
}
