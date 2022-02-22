using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button),typeof(Text))]
    public abstract class UIButton: MonoBehaviour
    {
        protected Button _button;
        private Text _text;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<Text>();
        }
        
        protected void TurnOff()
        {
            _button.interactable = false;
            _text.enabled = false;
        }

        protected void TurnOn()
        {
            _button.interactable = true;
            _text.enabled = true;
        }

        protected abstract void OnClick();
    }
}
