using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteractuable
{
    [SerializeField] private Transform moveTo;
    [SerializeField] private PlayerDetector playerDetector;
    [SerializeField] private BoxCollider col;
    public void Interact()
    {
        if (playerDetector.Player != null)
        {
            NavMeshAgent player = playerDetector.Player;
            player.Warp(moveTo.position);
            
        }
    }
}
