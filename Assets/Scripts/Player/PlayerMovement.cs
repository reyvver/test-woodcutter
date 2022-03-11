using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovement
    {
        public event Action<Vector3> PlayerStopped;
        
        private readonly NavMeshAgent _playerAgent;
        private readonly Transform _playerTransform;
        private Vector3 _destination;
        
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
            _destination = position;
            
            _playerAgent.isStopped = false;
            _playerAgent.SetDestination(_destination);
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
            PlayerStopped?.Invoke(_destination);
        }
        
        private bool WaitForDestinationReached()
        {
            return Vector3.Distance(_playerTransform.position, _destination) < DistanceFromObject;
        }
        
        public IEnumerator LookAtInteractableObject(Vector3 target)
        {
            float time = 0;
            while (time <= RotationTime) 
            {
                var direction = (target - _playerTransform.position).normalized;
                var lookRotation = Quaternion.LookRotation(direction);

                _playerTransform.rotation = Quaternion.Slerp(_playerTransform.rotation, lookRotation, Time.deltaTime * RotationSpeed);                
                time += Time.deltaTime;
       
                yield return null;
            }
        }
    }
}