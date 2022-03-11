using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovement
    {
        public event Action PlayerStopped;
        
        private NavMeshAgent _playerAgent;
        private Transform _playerTransform;
        private Vector3 destination;
        
        private const float DistanceFromObject = 1.5f;
        private const float RotationTime = 2f;
        private const float RotationSpeed = 3f;
        
        public PlayerMovement(NavMeshAgent agent)
        {
            _playerAgent = agent;
            _playerTransform = agent.transform;
        }

        public void PlaceAt(Vector3 position, Quaternion rotation)
        {
            _playerTransform.SetPositionAndRotation(position, rotation);
        }
        
        public void MoveTo(Vector3 position)
        {
            destination = position;
            
            _playerAgent.isStopped = false;
            _playerAgent.SetDestination(destination);
        }

        public IEnumerator WaitToStop()
        {
            yield return new WaitUntil(WaitForDestinationReached);
            yield return null;
            DestinationReached();
        }

        private void DestinationReached()
        {
            _playerAgent.isStopped = true;
            PlayerStopped?.Invoke();
        }
        

        private bool WaitForDestinationReached()
        {
            return Vector3.Distance(_playerTransform.position, destination) < DistanceFromObject;
        }
    }
}