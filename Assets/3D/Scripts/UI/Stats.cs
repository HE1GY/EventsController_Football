using PlayerScripts;
using ShitPalka;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Palka _palka;

        [SerializeField] private Text _stepsCountText;
        [SerializeField] private Text _speedText;
        [SerializeField] private Text _hitsText;

        private int _steps;
        private float _speed;
        private int _hitsPalka;


        private void OnEnable()
        {
            _player.MakeStep += IncrementSteps;
            _palka.SubscribeOnHit(IncrementHits);
        }

        private void OnDisable()
        {
            _player.MakeStep -= IncrementSteps;
            _palka.UnSubscribeOnHit(IncrementHits);
        }

        private void Update()
        {
            _speed = _player.GetSpeed();
            DisplayStats();
        }


        private void IncrementSteps()
        {
            _steps++;
        }

        private void IncrementHits()
        {
            _hitsPalka++;
        }

        private void DisplayStats()
        {
            _stepsCountText.text = _steps.ToString();
            _speedText.text = _speed.ToString();
            _hitsText.text = _hitsPalka.ToString();
        }
    }
}