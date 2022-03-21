using UnityEngine;

namespace Props
{
    public class Item : MonoBehaviour, IThrowable
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }

        public Transform Transform { get; set; }

        public void ThrowMe(float force, Vector3 direction)
        {
            transform.parent = null;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(force * direction, ForceMode.Impulse);
        }

        public void TakeMe(Transform placeHolder)
        {
            _rigidbody.isKinematic = true;
            transform.position = placeHolder.position;
            transform.SetParent(placeHolder);
        }
    }
}