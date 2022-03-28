using UnityEngine;

namespace Player
{
    public class GroundChecker
    {
        private const string _layerNameSnake = "Snake";
        private const int MaxDistance = 1;

        public bool IsOnSnake { get; private set; }

        private Transform _transform;

        public GroundChecker(Transform transform)
        {
            _transform = transform;
        }

        public void HandleGroundChecking()
        {
            Vector3 rayOrg = _transform.position;
            Vector3 rayDir = -_transform.up;
            Ray ray = new Ray(rayOrg, rayDir);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance, LayerMask.GetMask(_layerNameSnake)))
            {
                IsOnSnake = true;
                _transform.SetParent(raycastHit.transform);
            }
            else
            {
                IsOnSnake = false;
                _transform.SetParent(null);
            }
        }
    }
}