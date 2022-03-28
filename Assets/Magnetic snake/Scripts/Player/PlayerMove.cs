using UnityEngine;

namespace Player
{
    public class PlayerMove
    {
        private Rigidbody _rigidbody;

        public PlayerMove(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Jump(Vector3 destination)
        {
            float height;
            Transform transform = _rigidbody.transform;
            float displacement = Vector3.Distance(transform.position, destination);
            float displacementY = destination.y - transform.position.y;
            if (displacementY == 0)
            {
                height = displacement / 2;
            }
            else
            {
                height = displacement;
            }

            Vector3 displacementXZ =
                new Vector3(destination.x - transform.position.x, 0, destination.z - transform.position.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt((-2 * -Physics.gravity.magnitude * height));
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / -Physics.gravity.magnitude) +
                                                   Mathf.Sqrt(2 * (displacementY - height) / -Physics.gravity.magnitude));
            _rigidbody.velocity = velocityY + velocityXZ;
        }
    }
}