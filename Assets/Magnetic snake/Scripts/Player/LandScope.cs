using UnityEngine;

namespace Player
{
    public class LandScope : MonoBehaviour
    {
        private const int TurnSpeed = 2;
        public bool IsRotateting { get; set; }

        void Update()
        {
            if (IsRotateting)
            {
                transform.Rotate(Vector3.forward * TurnSpeed);
            }
        }
    }
}