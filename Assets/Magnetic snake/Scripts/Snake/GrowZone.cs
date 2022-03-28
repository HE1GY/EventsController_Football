using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Snake
{
    public class GrowZone : MonoBehaviour
    {
        public event Action Grow;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _color;
        [SerializeField] private Color _color2;


        private void Start()
        {
            Fading();
        }

        private async void Fading()
        {
            float t = 0;
            while (true)
            {
                if (t < 1)
                {
                    _meshRenderer.material.color = Color.Lerp(_color, _color2, t);
                    t += Time.deltaTime;
                }
                else if (t > 1)
                {
                    t = 0;
                }

                await Task.Yield();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out MagneticSegment magneticSegment))
            {
                Grow?.Invoke();
                Destroy(magneticSegment.gameObject);
            }
        }
    }
}