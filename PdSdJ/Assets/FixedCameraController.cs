using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class FixedCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private PlayerDetector _playerDetector;
    
    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        _playerDetector.OnPlayerDetected += TurnCamera;
    }

    private void OnDisable()
    {
        _playerDetector.OnPlayerDetected -= TurnCamera;
    }

    private void TurnCamera(CharacterController characterController)
    {
        bool turnOn = characterController != null;
        camera.enabled = turnOn;
    }
}
