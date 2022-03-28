using System;
using Props;
using UnityEngine;

namespace PlayerScripts
{
    public class TakeThrower
    {
        private const int MaxDistance = 100;
        public event Action<bool> CanTake;
        public event Action<Vector3> TakeItem;
        public event Action ThrowItem;

        private readonly Transform _placeHolder;
        private readonly Transform _camTransform;
        private readonly LayerMask _mask;
        private readonly float _throwForce;
        private readonly float _takeDistance;

        private IThrowable _iThrowableItem;

        public TakeThrower(Transform placeHolder, Transform camTransform, LayerMask mask, PlayerInput3D input,
            float throwForce, float takeDistance)
        {
            _placeHolder = placeHolder;
            _camTransform = camTransform;
            _mask = mask;
            _throwForce = throwForce;
            _takeDistance = takeDistance;


            input.Player.TakeItem.performed += _ => TakingItem();

            input.Player.ThrowItem.performed += _ =>
            {
                if (_iThrowableItem != null)
                {
                    ThrowItem?.Invoke();
                }
            };

            input.Player.CameraRotation.performed += _ =>
            {
                bool canTake = TryTake(out IThrowable item);
                CanTake?.Invoke(canTake);
            };
        }

        #region Animation Methods

        public void FinallyTake()
        {
            _iThrowableItem.TakeMe(_placeHolder);
        }

        public void FinallyThrow()
        {
            _iThrowableItem.ThrowMe(_throwForce, _camTransform.forward);
            _iThrowableItem = null;
        }

        #endregion


        private bool TryTake(out IThrowable item)
        {
            Ray ray = new Ray(_camTransform.position, _camTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance, _mask))
            {
                float distanceToItem = Vector3.Distance(_placeHolder.position, raycastHit.point);
                if (distanceToItem <= _takeDistance)
                {
                    if (raycastHit.collider.gameObject.TryGetComponent<IThrowable>(out IThrowable item1))
                    {
                        item = item1;
                        return true;
                    }
                }
            }

            item = null;
            return false;
        }

        private void TakingItem()
        {
            if (TryTake(out IThrowable item))
            {
                if (_iThrowableItem == null)
                {
                    _iThrowableItem = item;
                    TakeItem?.Invoke(item.Transform.position);
                }
            }
        }
    }
}