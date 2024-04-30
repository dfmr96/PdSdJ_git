using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Collider))]
public class PlayerDetector : MonoBehaviour
{
    [field: SerializeField] public CharacterController Player { get; private set; }
    public event Action<CharacterController> OnPlayerDetected;
    private Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController playerController))
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
