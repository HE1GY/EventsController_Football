using System;
using Core;
using UnityEngine;

namespace Architecture.Stats
{
    public class StatsManager : MonoBehaviour
    {
        public event Action<string> ScoreChange;
        
        [SerializeField] private int _lastGoal;
        
        private GameStats _gameStats;

        private void Awake() => _gameStats= new GameStats(_lastGoal);

        private void OnEnable()
        {
            EventsController.AddListener(EventsType.PlayerScoreGoal,PlayerScore);
            EventsController.AddListener(EventsType.EnemyScoreGoal,EnemyScore);
            EventsController.AddListener(EventsType.ResetGame,ResetStats);
        }

        private void OnDisable()
        {
            EventsController.RemoveListener(EventsType.PlayerScoreGoal,PlayerScore);
            EventsController.RemoveListener(EventsType.EnemyScoreGoal,EnemyScore);
            EventsController.RemoveListener(EventsType.ResetGame,ResetStats);
        }

        public string GetWinnerName() => _gameStats.Winner;

        private void PlayerScore()
        {
            _gameStats.ScoreBy(CreatureSide.Player);
            ScoreChange?.Invoke(GetScore());
        }

        private void EnemyScore()
        {
            _gameStats.ScoreBy(CreatureSide.Enemy);
            ScoreChange?.Invoke(GetScore());
        }

        private void ResetStats()
        {
            _gameStats.ResetStats();
            ScoreChange?.Invoke(GetScore());
        }

        private string GetScore() => _gameStats.ToString();
    }
}