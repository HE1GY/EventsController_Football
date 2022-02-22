using Core;

namespace UI.Buttons
{
   public class PlayButton : UIButton
   {
      private void OnEnable()
      {
         EventsController.AddListener(EventsType.PauseGame,TurnOn);
         EventsController.AddListener(EventsType.ResetGame,TurnOn);
         EventsController.AddListener(EventsType.PlayGame,TurnOff);
      
         _button.onClick.AddListener(OnClick);
      }

      private void OnDisable()
      {
         EventsController.RemoveListener(EventsType.PauseGame,TurnOn);
         EventsController.RemoveListener(EventsType.ResetGame,TurnOn);
         EventsController.RemoveListener(EventsType.PlayGame,TurnOff);
      
         _button.onClick.RemoveListener(OnClick);
      }

      protected override void OnClick() => EventsController.Broadcast(EventsType.PlayGame);
   }
}
