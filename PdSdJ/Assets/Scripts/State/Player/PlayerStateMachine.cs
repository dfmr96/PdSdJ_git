using System;
using UnityEngine;

namespace State.Player
{
     [Serializable]
     public class PlayerStateMachine : StateMachine
     {
          [field: SerializeField] public IdleState IdleState { get; private set; }
          [field: SerializeField] public WalkingState WalkingState { get; private set; }

          
          [field: SerializeField] public CharacterController Controller { get; private set; }
          public PlayerStateMachine(CharacterController controller)
          {
               Controller = controller;
               IdleState = new IdleState(this);
               WalkingState = new WalkingState(this);
               Initialize(IdleState);
          }

     }
}