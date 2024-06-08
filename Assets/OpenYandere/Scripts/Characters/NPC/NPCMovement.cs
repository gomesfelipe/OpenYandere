using OpenYandere.Characters.Player;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace OpenYandere.Characters.NPC
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCMovement : MonoBehaviour
    {
        private AnimatorData _animatorData;
        [SerializeField] protected NPC _npc;        
        
        public NavMeshAgent NavigationAgent => _navMeshAgent;
        public GameObject player;

        [Header("References:")]
        [SerializeField] private CharacterAnimator _characterAnimator;
        [SerializeField] protected NavMeshAgent _navMeshAgent;
        
        [Header("Settings:")]
        [Tooltip("The walking speed of the NPC.")]
        public float WalkSpeed = 4.0f;
        [Tooltip("The running speed of the NPC.")]
        public float RunSpeed = 12.0f;
        [Tooltip("Is the NPC running?")]
        public bool IsRunning;
        
        protected void Awake()
        {
            _navMeshAgent.updateRotation = true;
            _navMeshAgent.updatePosition = true;
            _npc=GetComponent<NPC>();
        }
        
        protected void FixedUpdate()
        {
            IsRunning = _npc.isInDanger;
            HandleMovement();
        }
        protected void HandleMovement()
        {

            // Get the current velocity of the navigation agent.
            var horizontalAxis = _navMeshAgent.velocity.x;
            var verticalAxis = _navMeshAgent.velocity.z;

            // Check if either axis is more than zero - meaning the NPC is moving.
            if (Math.Abs(horizontalAxis) > 0f || Math.Abs(verticalAxis) > 0f)
            {
                // Get the speed to move at.
                var movementSpeed = IsRunning ? RunSpeed : WalkSpeed;

                // Calculate the direction to move in.
                var moveDirection = new Vector3(horizontalAxis, 0, verticalAxis);

                // Update the speed of the navigation agent.
                _navMeshAgent.speed = movementSpeed;

                // Update the animator entry.
                _animatorData.IsRunning = IsRunning;
                _animatorData.MoveDirection = moveDirection;
            }
            else
            {
                // Update the animator data.
                _animatorData.IsRunning = false;
                _animatorData.MoveDirection = Vector3.zero;
            }

            // Update the data the character animator is using.
            _characterAnimator.UpdateData(_animatorData);
        }


        public void FleeFromPlayer()
        {
            Vector3 fleeDirection = AIBehaviourFlee.FleeFrom(transform.position, player.transform.position);
            float fleeDistance = 20f; // Distância fixa de fuga

            Vector3 potentialFleePosition = transform.position + new Vector3(fleeDirection.x, 0, fleeDirection.y) * fleeDistance;

            // Incluindo camadas "Obstacle" e "Default" na máscara
            int layerMask = LayerMask.GetMask("Obstacle") | LayerMask.GetMask("Default");
            Collider[] obstacles = Physics.OverlapSphere(transform.position, 30f, layerMask);
            Vector3 bestHidingPosition = AIBehaviourHide.ClosestHidingPosition(transform.position, player.transform.position, obstacles, obstacles.Length);

            if (bestHidingPosition != transform.position)
            {
                _navMeshAgent.speed = RunSpeed;
                _navMeshAgent.SetDestination(bestHidingPosition);
                _animatorData.IsRunning = true;
                _characterAnimator.UpdateData(_animatorData);
            }
            else
            {
                // Se não encontrar uma posição de esconderijo válida, fugir para a posição de fuga potencial
                _navMeshAgent.speed = RunSpeed;
                _navMeshAgent.SetDestination(potentialFleePosition);
                _animatorData.IsRunning = true;
                _characterAnimator.UpdateData(_animatorData);
            }
        }

        public void Resume()
        {
            // Resume navigation agent.
            _navMeshAgent.isStopped = false;
            
            // Resume the animator.
            _characterAnimator.Resume();
        }

        public void Pause()
        {
            // Pause the navigation agent.
            _navMeshAgent.isStopped = true;
            
            // Pause the animator.
            _characterAnimator.Pause();
        }

    }
}