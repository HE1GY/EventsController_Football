using UnityEngine;

namespace Player
{
    public class LegAnimation
    {
        private Animator _animator;
        private static readonly int _catch = Animator.StringToHash("Catch");

        public LegAnimation(Animator animator)
        { 
            _animator = animator;
        }

        public void PlayCatch()
        {
            _animator.SetTrigger(_catch);
        }
    }
}