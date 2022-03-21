using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PlayerScripts
{
    public class PlayerIK
    {
        private readonly Transform _rArmTarget;
        private readonly Rig _rArmRig;
        
        public PlayerIK(Rig rArmRig)
        {
            _rArmRig = rArmRig;
            _rArmTarget = _rArmRig.GetComponentInChildren<TwoBoneIKConstraint>().data.target;
        }

        public void SetActiveArmRig()
        {
            _rArmRig.weight = 1;
        }

        public void SetUnActiveArmRig()
        {
            _rArmRig.weight = 0;
        }

        public void SetIKArmTarget(Vector3 targetPosition)
        {
            _rArmTarget.position = targetPosition;
        }
    }
}