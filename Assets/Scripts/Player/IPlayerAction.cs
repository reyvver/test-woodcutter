using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public interface IPlayerAction
    {
        public IEnumerator DoAnimation(Action<bool> callback);
    }

    public class TreeAction : IPlayerAction
    {
        private PlayerAnimation animator { get; }

        public TreeAction(PlayerAnimation animation)
        {
            animator = animation;
        }

        public IEnumerator DoAnimation(Action<bool> callback)
        {
            for (int i = 1; i <= 3; i++)
            {
                animator.PlayAnimation(PlayerAnimation.AnimationType.Chop);
                yield return new WaitForSeconds(animator.lenght);
            }

            yield return null;
            callback(true);
        }

    }
}