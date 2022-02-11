using Architecture.Stats;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class WinnerNameText : MonoBehaviour
    {
        [SerializeField] private StatsManager _statsManager;
        private Text _nameText;

        private void Awake()
        {
            _nameText = GetComponent<Text>();
        }

        private void OnEnable()
        {
            EventsController.AddListener(EventsType.EndGame, SetWinnerName);
            EventsController.AddListener(EventsType.EndGame, TurnOn);
            EventsController.AddListener(EventsType.ResetGame, TurnOff);
        }

        private void OnDisable()
        {
            EventsController.RemoveListener(EventsType.EndGame, SetWinnerName);
            EventsController.RemoveListener(EventsType.EndGame, TurnOn);
            EventsController.RemoveListener(EventsType.ResetGame, TurnOff);
        }


        public void SetWinnerName() => _nameText.text = _statsManager.GetWinnerName() + " Win";

        private void TurnOff() => _nameText.enabled = false;
        private void TurnOn() => _nameText.enabled = true;
    }
}