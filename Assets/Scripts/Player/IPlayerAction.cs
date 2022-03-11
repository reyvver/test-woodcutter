using System;
using System.Collections;
using InteractableObjects;
using UnityEngine;

namespace Player
{
    public interface IPlayerAction
    {
        public IEnumerator DoAnimation(Action<bool> callback, IInteractable obj);
    }

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


    public class WoodAction  : IPlayerAction
    {
        private PlayerAnimation animator { get; }

        public WoodAction(PlayerAnimation animation)
        {
            animator = animation;
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