using UnityEngine;

namespace State.Player
{
    public class IdleState : StateBase
    {
        private PlayerStateMachine playerStateMachine;

        public IdleState(PlayerStateMachine playerStateMachine)
        {
            this.playerStateMachine = playerStateMachine;
            stateName = "Idle";
        } 
        public override void Enter()
        {
            Debug.Log("Idle State enter");
        }

        public override void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                playerStateMachine.ChangeStateTo(playerStateMachine.WalkingState);
            }
            
            if (Input.GetButtonDown("Aim"))
            {
                playerStateMachine.ChangeStateTo(playerStateMachine.AimingState);
            }
            
            Debug.Log("Idle State Update");
        }

        public override void Exit()
        {
            Debug.Log("Idle State exit");
        }
    }
}