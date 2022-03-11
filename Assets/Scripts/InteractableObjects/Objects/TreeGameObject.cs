using System.Collections.Generic;
using UnityEngine;

namespace InteractableObjects
{
    public class TreeGameObject : MonoBehaviour
    {
        public List<Transform> wood;
        
        private float _hitTime = 1f;
        private float _hitTimer = 0;
        private bool _canHit = true;

        private void OnCollisionEnter(Collision collision)
        {
            TakeDamage();
        }
        
        void Update()
        {
            _hitTimer += Time.deltaTime;
            
            if (_hitTimer > _hitTime)
                _canHit = true;
        }
 
        void TakeDamage()
        {
             if(!_canHit) return;

            _hitTimer = 0;
            _canHit = false;
        }

    }
}