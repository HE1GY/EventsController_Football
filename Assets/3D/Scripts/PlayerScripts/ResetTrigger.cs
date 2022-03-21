using UnityEngine;

namespace PlayerScripts
{
    public class ResetTrigger : StateMachineBehaviour
    {
        private readonly int _landedTriggerHash = Animator.StringToHash("landed");

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.ResetTrigger(_landedTriggerHash);
        }
    }
}