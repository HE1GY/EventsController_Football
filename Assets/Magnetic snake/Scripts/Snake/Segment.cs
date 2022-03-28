using UnityEngine;

namespace Snake
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public void HandleSegmentMove(Vector3 position)
        {
            transform.LookAt(position);
            transform.position = position;
        }
    }
}