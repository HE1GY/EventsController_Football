using UnityEngine;

namespace Snake
{
    public class SnakeMover
    {
        private float _speed;
        private Transform _transform;

        private float _turnSpeed = 0.5f;
        private Vector3 _lookDirection;

        public SnakeMover(Rigidbody rigidbody, float speed)
        {
            _transform = rigidbody.transform;
            _speed = speed;
        }

        public void SetLookDirection(Vector3 lookDirection)
        {
            _lookDirection = lookDirection;
        }


        public void HandleMovement()
        {
            _transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        public void HandleRotation()
        {
            if (_lookDirection != Vector3.zero)
            {
                _lookDirection.y = 0;
                Quaternion lookDirection = Quaternion.LookRotation(_lookDirection);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookDirection, Time.deltaTime * _turnSpeed);
            }
        }
    }
}