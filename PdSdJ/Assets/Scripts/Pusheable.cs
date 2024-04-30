using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Pusheable : MonoBehaviour, IInteractuable
{
    public Vector3 Direction { get;private set; }
    private float speed;
    public List<PusheableDirectioner> directioners;
    [FormerlySerializedAs("player")] public NavMeshAgent playerAgent;
    public float distance;
    private void Start()
    {
        foreach (PusheableDirectioner directioner in directioners)
        {
            directioner.OnDirectionSet += SetDirection;
        }
    }

    public void Interact()
    {
        
    }

    public void SetDirection(CharacterController characterController, Vector3 direction)
    {
        playerAgent = characterController.Agent;
        if (playerAgent == null)
        {
            Direction = Vector3.zero;
            return;
        }
        Direction = direction;
    }

    private void OnDestroy()
    {
        foreach (PusheableDirectioner directioner in directioners)
        {
            directioner.OnDirectionSet -= SetDirection;
        }
    }
}
