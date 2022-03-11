using System;
using System.Collections;
using InteractableObjects;
using UnityEngine;

namespace Player
{
    public class WoodAction  : IPlayerAction
    {
        private PlayerAnimation animator { get; }
        private readonly PlayerLogPlacement _logPlacement;

        public WoodAction(PlayerAnimation animation, PlayerLogPlacement logPlacement)
        {
            animator = animation;
            _logPlacement = logPlacement;
        }

        public IEnumerator DoAnimation(Action<bool> callback, IInteractable obj)
        {
            animator.PlayAnimation(PlayerAnimation.AnimationType.PickUp);

            while (animator.stateChanging)
            {
                yield return null;
            }

            float counter = 0;
            var waitTime = animator.time;

            while (counter < (waitTime)/5)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            
            obj.Hide();
            
            while (counter < (waitTime/2))
            {
                counter += Time.deltaTime;
                yield return null;
            }
            
            obj.Interact();
            _logPlacement.Place();
            
            callback(true);
        }
    }
}