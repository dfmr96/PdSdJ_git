using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pusheable : MonoBehaviour, IInteractuable
{
    public Vector3 Direction { get;private set; }
    private float speed;
    public List<PusheableDirectioner> directioners;
    public NavMeshAgent player;
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

    public void SetDirection(NavMeshAgent agent, Vector3 direction)
    {
        player = agent;
        if (player == null)
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
