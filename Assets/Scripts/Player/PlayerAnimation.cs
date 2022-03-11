using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        public float lenght => _animator.GetCurrentAnimatorStateInfo(0).length;
        private readonly Animator _animator;

        private const string Walk = "IsWalking";
        private const string Chop = "Chop";


        public PlayerAnimation(Animator playerAnimator)
        {
            _animator = playerAnimator;
        }

        public enum AnimationType
        {
            Idle,
            Walk,
            Chop,
            Get
        }

        public void PlayAnimation(AnimationType animationType)
        {
            SetIdleAnimation();

            switch (animationType)
            {
                case AnimationType.Walk:
                {
                    _animator.ResetTrigger(Chop);
                    _animator.SetBool(Walk, true);

                    break;
                }
                case AnimationType.Chop:
                {
                    _animator.SetTrigger(Chop);

                    break;
                }
            }
        }

        private void SetIdleAnimation()
        {
            _animator.SetBool(Walk, false);
            _animator.SetBool(Chop, false);
        }

    }
}