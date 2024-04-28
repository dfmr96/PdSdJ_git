using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetector : MonoBehaviour
{
    [field: SerializeField] public NavMeshAgent Player { get; private set; }
    public Action<NavMeshAgent> OnPlayerDetected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent playerController))
        {
            Player = playerController;
            OnPlayerDetected?.Invoke(Player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Player == null) return;
        
        if (other.gameObject == Player.gameObject)
        {
            Player = null;
            OnPlayerDetected?.Invoke(null);
        }
    }
}
