using System;
using System.Collections;
using InteractableObjects;
using UnityEngine;

namespace Player
{
    public class TreeAction : IPlayerAction
    {
        private PlayerAnimation animator { get; }

        public TreeAction(PlayerAnimation animation)
        {
            animator = animation;
        }

        public IEnumerator DoAnimation(Action<bool> callback, IInteractable obj)
        {
            float counter = 0;
            float waitTime = 0;
            
            for (int i = 1; i <= 3; i++)
            {
                animator.PlayAnimation(PlayerAnimation.AnimationType.Chop);
                
                while (animator.stateChanging)
                {
                    yield return null;
                }
                counter = 0;
                waitTime = animator.time;

                if (i == 3)
                    waitTime = waitTime / 4;

                while (counter < (waitTime))
                {
                    counter += Time.deltaTime;
                    yield return null;
                }
            }
            
            waitTime = animator.time;
            obj.Hide();

            while (counter < (waitTime))
            {
                counter += Time.deltaTime;
                yield return null;
            }
            
            obj.Interact();

            callback(true);
        }

    }
}