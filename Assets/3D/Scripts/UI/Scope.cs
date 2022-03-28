using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Scope : MonoBehaviour
    {
        [SerializeField] private PlayerScripts.Player _player;
        [SerializeField] private Color _idle;
        [SerializeField] private Color _takable;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            _player.SubscribeOnCanTake(OnCanTake);
        }

        private void OnDisable()
        {
            _player.UnSubscribeOnCanTake(OnCanTake);
        }

        private void OnCanTake(bool canTake)
        {
            if (canTake)
            {
                _image.color = _takable;
            }
            else
            {
                _image.color = _idle;
            }
        }
    }
}