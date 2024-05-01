using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PusheableDirectioner : MonoBehaviour
{
    [SerializeField] private PlayerDetector playerDetector;
    public event Action<CharacterController,Vector3> OnDirectionSet;
    [SerializeField] private Transform pusheable;
    private void Start()
    {
        playerDetector = GetComponent<PlayerDetector>();
        playerDetector.OnPlayerDetected += SetDirection;
        pusheable = transform.parent;
    }

    public void SetDirection(CharacterController playerController)
    {
        Vector3 direction = (pusheable.position - transform.position).normalized;
        
        OnDirectionSet?.Invoke(playerController,direction);
    }
}
