using System;
using System.Collections;
using System.Collections.Generic;
using State.Player;
using UnityEngine;

public class PushInteractor : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float pushTimer;
    [SerializeField] private float pushCountdown;
    [field: SerializeField] public Pusheable pusheableSelected { get; private set; }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Pusheable pusheableObj))
        {
            pusheableSelected = pusheableObj;
            pushCountdown = pushTimer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Pusheable pusheableObj))
        {
            pusheableSelected = null;
            pushCountdown = pushTimer;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Pusheable pusheableObj))
        {

            if (Input.GetAxisRaw("Vertical") >= 1)
            {
                pushCountdown -= Time.deltaTime;
                
                if (pushCountdown <= 0)
                {
                    PlayerStateMachine playerStateMachine = controller.StateMachine;
                    playerStateMachine.ChangeStateTo(playerStateMachine.PushState);
                    pushCountdown = pushTimer;
                }
            }

            /*float distance = (pusheableObj.transform.position - transform.position).magnitude;
            if (distance < 1)
            {
                if (Input.GetAxisRaw("Vertical") >= 1)
                {
                    pushTimer -= Time.deltaTime;

                    if (pushTimer <= 0)
                    {
                        playerStateMachine.ChangeStateTo(playerStateMachine.PushState);
                    }
                }
            } */
        }
    }
}
