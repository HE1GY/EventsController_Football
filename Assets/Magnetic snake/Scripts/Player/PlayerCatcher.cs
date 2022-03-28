using System;
using UnityEngine;

namespace Player
{
    public class PlayerCatcher
    {
        public event Action<Vector3> Catch;

        private const int DefaultLayer = 0;
        private const int MagneticLayer = 7;

        private readonly Transform _placeHolder;
        private Rigidbody _currentRigidbody;
        private Collider _currentCollider;

        private bool _catchPress;

        public PlayerCatcher(Transform placeHolder, PlayerInput playerInput)
        {
            _placeHolder = placeHolder;

            playerInput.Player.Catch.started += ctx => _catchPress = ctx.ReadValueAsButton();
            playerInput.Player.Catch.canceled += ctx =>
            {
                _catchPress = ctx.ReadValueAsButton();
                if (_currentRigidbody != null)
                {
                    Throw();
                }
            };
        }

        public void OnCanCatch(bool canCatch, Vector3 itemPosition, Rigidbody rigidbody)
        {
            if (canCatch && _catchPress)
            {
                FinallyCatch(rigidbody);
                Catch?.Invoke(itemPosition);
            }
        }

        private void FinallyCatch(Rigidbody rigidbody)
        {
            _currentCollider = rigidbody.gameObject.GetComponent<Collider>();
            rigidbody.isKinematic = true;
            rigidbody.transform.SetParent(_placeHolder);
            _currentRigidbody = rigidbody;
            rigidbody.gameObject.layer = DefaultLayer;
            _currentCollider.enabled = false;
        }


        private void Throw()
        {
            _currentRigidbody.gameObject.layer = MagneticLayer;
            _currentRigidbody.isKinematic = false;
            _currentRigidbody.transform.parent = null;
            _currentCollider.enabled = true;
        }
    }
}