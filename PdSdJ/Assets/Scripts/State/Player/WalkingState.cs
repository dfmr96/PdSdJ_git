using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

namespace State.Player
{
    public class WalkingState : StateBase
    {
        private readonly PlayerStateMachine _playerStateMachine;
        
        private readonly CharacterController _controller;
        private NavMeshAgent _agent;
        private Transform _playerTransform;
        private readonly float _walkingSpeed;
        private readonly float _angularSpeed;
        
        public WalkingState(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
            _controller = playerStateMachine.Controller;
            _agent = _controller.GetComponent<NavMeshAgent>();
            _playerTransform = _controller.transform;
            _walkingSpeed = _controller.PlayerStats.walkingSpeed;
            _angularSpeed = _controller.PlayerStats.angularSpeed;
        } 
        public override void Enter()
        {
            _agent = _controller.GetComponent<NavMeshAgent>();
            _playerTransform = _controller.transform;
        }

        public override void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal == 0 && vertical == 0)
            {
                _playerStateMachine.ChangeStateTo(_playerStateMachine.IdleState);
            }
            
            Walk(vertical, horizontal);
            
            Debug.Log("Walking State Update");
        }

        private void Walk(float vertical, float horizontal)
        {
            _agent.Move(_playerTransform.forward * (vertical * _walkingSpeed * Time.deltaTime));
            _playerTransform.Rotate(Vector3.up * (horizontal * _angularSpeed * Time.deltaTime));
        }

        public override void Exit()
        {
            Debug.Log("Walking State exit");
        }
    }
}