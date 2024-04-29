using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pusheable : MonoBehaviour, IInteractuable
{
    public Vector3 direction;
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
        //if (player == null) return;

        //speed = player.GetComponent<CharacterController>().currentSpeed;
    }

    private void Update()
    {
        TryToMove();
    }

    public void TryToMove()
    {
        if (speed == 0 || player == null) return;
        
        distance = (player.transform.position - transform.position).magnitude;

        if (distance < 1)
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    }

    public void SetDirection(NavMeshAgent agent, Vector3 direction)
    {
        player = agent;
        if (player == null)
        {
            this.direction = Vector3.zero;
            return;
        }
        CharacterController playerController = agent.GetComponent<CharacterController>();
        playerController.PushObject(true);
        speed = playerController.CurrentSpeed;
        this.direction = direction;
        //speed = 0;
        //direction = new Vector3(MathF.Round(direction.x), MathF.Round(direction.y), MathF.Round(direction.z));
    }

    private void OnDestroy()
    {
        foreach (PusheableDirectioner directioner in directioners)
        {
            directioner.OnDirectionSet -= SetDirection;
        }
    }
}
