using System;
using UnityEngine;

namespace Player
{
    public class LandScopePlacer
    {
        public event Action<Vector3> Jump;
    
        private  LayerMask _layerMask=~(6<<9);
        private const int MaxDistance = 100;
        
        private readonly Vector3 _offset = new Vector3(0, 0.001f,0);

        private LandScope _landScope;
        private Transform _camera;
        private Vector3 _currentJumpPlace;

        public LandScopePlacer(Transform camera, LandScope landScope, PlayerInput playerInput)
        {
            _camera = camera;
            _landScope = landScope;
            playerInput.Player.Jump.started+=_=>Jump?.Invoke(_currentJumpPlace);
        }

        public void HandleLandScopePlacing()
        {
            Vector3 rayOrg = _camera.position;
            Vector3 rayDir = _camera.forward;
            Ray ray = new Ray(rayOrg, rayDir);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance,_layerMask))
            {
                _landScope.transform.position = raycastHit.point+_offset;
                _currentJumpPlace = raycastHit.point;
                _landScope.IsRotateting = true;
            }
            else
            {
                _landScope.IsRotateting = false;
            }
        }
    }
}