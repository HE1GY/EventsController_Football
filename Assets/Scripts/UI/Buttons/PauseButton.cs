using Core;

namespace UI.Buttons
{
   public class PauseButton : UIButton
   {
      private void OnEnable()
      {
         EventsController.AddListener(EventsType.EndGame,TurnOff);
         EventsController.AddListener(EventsType.PauseGame,TurnOff);
         EventsController.AddListener(EventsType.PlayGame,TurnOn);
      
         _button.onClick.AddListener(OnClick);
      
      }

      private void OnDisable()
      {
         EventsController.RemoveListener(EventsType.EndGame,TurnOff);
         EventsController.RemoveListener(EventsType.PauseGame,TurnOff);
         EventsController.RemoveListener(EventsType.PlayGame,TurnOn);
      
         _button.onClick.RemoveListener(OnClick);
      }
   

      protected override void OnClick() => EventsController.Broadcast(EventsType.PauseGame);
   }
}
