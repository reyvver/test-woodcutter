using UnityEngine;

namespace Player
{
    public class PlayerAnimation
    {
        private readonly Animator _animator;

        public bool stateChanging => _animator.GetCurrentAnimatorStateInfo(0).fullPathHash != animHash;
        public float time => _animator.GetCurrentAnimatorStateInfo(0).length;
        
        private const string Walk = "IsWalking";
        private const string Chop = "Chop";
        private const string PickUp = "PickUp";
        private const string AnimBaseLayer = "Base Layer.";
        
        private int walkAnimHash;
        private int chopAnimHash;
        private int pickAnimHash;
        private int animHash;


        public PlayerAnimation(Animator playerAnimator)
        {
            _animator = playerAnimator;
            walkAnimHash = Animator.StringToHash(AnimBaseLayer + Walk);
            chopAnimHash = Animator.StringToHash(AnimBaseLayer + Chop);
            pickAnimHash = Animator.StringToHash(AnimBaseLayer + PickUp);
        }

        public enum AnimationType
        {
            Idle,
            Walk,
            Chop,
            PickUp
        }

        public void PlayAnimation(AnimationType animationType)
        {
            SetIdleAnimation();

            switch (animationType)
            {
                case AnimationType.Walk:
                {
                    animHash = walkAnimHash;
                    _animator.SetBool(Walk, true);
                    break;
                }
                case AnimationType.Chop:
                {
                    animHash = chopAnimHash;
                    _animator.CrossFadeInFixedTime(Chop, 0.6f);
                    break;
                }
                case AnimationType.PickUp:
                {
                    animHash = pickAnimHash;
                    _animator.CrossFadeInFixedTime(PickUp, 0.6f);
                    break;
                }
            }
        }

        private void SetIdleAnimation()
        {
            _animator.ResetTrigger(Chop);
            _animator.ResetTrigger(PickUp);
            _animator.SetBool(Walk, false);
        }

    }
}