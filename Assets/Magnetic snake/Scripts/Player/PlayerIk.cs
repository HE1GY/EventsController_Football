using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player
{
    class PlayerIk
    {
        public event Action Catch;

        private Rig _handRig;
        private Transform _handTarget;

        public PlayerIk(Rig handRig)
        {
            _handRig = handRig;
            _handTarget = _handRig.GetComponentInChildren<ChainIKConstraint>().data.target;
        }


        public void OnCatch(Vector3 itemPosition)
        {
            _handTarget.position = itemPosition;
            Catch?.Invoke();
        }
    }
}