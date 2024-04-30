using DefaultNamespace;
using UnityEngine;

namespace State.Player
{
    public class PushState : StateBase
    {
        private PlayerStateMachine playerStateMachine;
        private CharacterController _controller;
        private PushInteractor _pushInteractor;
        private Pusheable objectToPush;
        private Vector3 direction;
        
        public PushState(PlayerStateMachine playerStateMachine)
        {
            this.playerStateMachine = playerStateMachine;
            _controller = playerStateMachine.Controller;
            _pushInteractor = _controller.PushInteractor;
        }

        public override void Enter()
        {
            objectToPush = _pushInteractor.pusheableSelected;
            direction = objectToPush.Direction;
            Debug.Log($"{direction}");
            _controller.transform.rotation = Quaternion.LookRotation(direction);
        }

        public override void Update()
        {
            if (Input.GetAxisRaw("Vertical") >= 1)
            {
                Vector3 direction = _controller.transform.forward * _controller.PlayerStats.pushingSpeed *
                                    Time.deltaTime;
                _controller.Agent.Move(direction);
                objectToPush.transform.Translate(direction);
            }

            if (Input.GetAxisRaw("Vertical") < 1)
            {
                playerStateMachine.ChangeStateTo(playerStateMachine.IdleState);
            }
        }

        public override void Exit()
        {
            objectToPush = null;
            direction = Vector3.zero;
        }
    }
}