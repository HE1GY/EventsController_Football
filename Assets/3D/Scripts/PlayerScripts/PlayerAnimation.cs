using UnityEngine;

namespace PlayerScripts
{
    public class PlayerAnimation
    {
        private const string TakingAnimationName = "Taking";
        private const int ThrowLayerIndex = 1;

        private readonly int _velocityHorizontalHash = Animator.StringToHash("horizontalVelocity");
        private readonly int _velocityVerticalHash = Animator.StringToHash("verticalVelocity");
        private readonly int _landedTriggerHash = Animator.StringToHash("landed");
        private readonly int _throwTriggerHash = Animator.StringToHash("throw");

        private readonly Animator _animator;

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void SetHorizontalVelocity(float velocity)
        {
            _animator.SetFloat(_velocityHorizontalHash, velocity);
        }

        public void SetVerticalVelocity(float velocity)
        {
            _animator.SetFloat(_velocityVerticalHash, velocity);
        }

        public void SetLandedTrigger()
        {
            _animator.SetTrigger(_landedTriggerHash);
        }

        public void PlayTaking()
        {
            _animator.Play(TakingAnimationName);
        }

        public void PlayThrowing()
        {
            _animator.SetLayerWeight(ThrowLayerIndex, 1);
            _animator.SetTrigger(_throwTriggerHash);
        }

        public void BackFromThrowingLayer()
        {
            _animator.SetLayerWeight(ThrowLayerIndex, 0);
        }
    }
}