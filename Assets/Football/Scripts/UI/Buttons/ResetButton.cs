using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button),typeof(Text))]
    public class ResetButton : UIButton
    {
        private void OnEnable()
        {
            EventsController.AddListener(EventsType.EndGame,TurnOn);
            EventsController.AddListener(EventsType.PauseGame,TurnOn);
            EventsController.AddListener(EventsType.ResetGame,TurnOff);
            EventsController.AddListener(EventsType.PlayGame,TurnOff);
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            EventsController.RemoveListener(EventsType.EndGame,TurnOn);
            EventsController.RemoveListener(EventsType.PauseGame,TurnOn);
            EventsController.RemoveListener(EventsType.ResetGame,TurnOff);
            EventsController.RemoveListener(EventsType.PlayGame,TurnOff);
            
            _button.onClick.RemoveListener(OnClick);
        }

        protected override void OnClick() => EventsController.Broadcast(EventsType.ResetGame);
    }
}
