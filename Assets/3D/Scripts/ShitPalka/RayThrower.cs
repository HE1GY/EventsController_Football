using System;
using UnityEngine;

namespace ShitPalka
{
    public class RayThrower
    {
        private const int MaxDistance = 100;
        private Transform _rayOrg;
        private Vector3 _rayDir;
        private LayerMask _layerMask;

        public RayThrower(Transform rayOrg, Vector3 rayDir, LayerMask layerMask)
        {
            _rayOrg = rayOrg;
            _rayDir = rayDir;
            _layerMask = layerMask;
        }

        public bool TryGetHitInfo(out RaycastHit raycastHit)
        {
            Ray ray;
            ray = new Ray(_rayOrg.position, _rayDir);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, MaxDistance, _layerMask))
            {
                raycastHit = hitInfo;
                return true;
            }

            raycastHit = new RaycastHit();
            return false;
        }

        public Vector3 GetRayHitPosition()
        {
            if (TryGetHitInfo(out RaycastHit raycastHit))
            {
                return raycastHit.point;
            }

            throw new Exception($"Ray does not hit mask{_layerMask}");
        }
    }
}