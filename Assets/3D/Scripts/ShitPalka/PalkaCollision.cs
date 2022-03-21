using System;
using Props;
using UnityEngine;

namespace ShitPalka
{
    public class PalkaCollision
    {
        public event Action Hit;

        public void HandleCollision(ControllerColliderHit hit)
        {
            if (hit.rigidbody != null && hit.rigidbody.velocity != Vector3.zero)
            {
                if (hit.gameObject.TryGetComponent(out IThrowable throwable))
                {
                    Hit?.Invoke();
                }
            }
        }
    }
}