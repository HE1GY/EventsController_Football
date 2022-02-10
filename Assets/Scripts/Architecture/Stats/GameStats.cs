using Core;

namespace Architecture.Stats
{
    public class GameStats
    {
        private readonly int _goalsToWin;

        public string Winner { get; private set; }
        
        private int _playerGoalCount;
        private int _enemyGoalCount;

        public GameStats(int goalsToWin)
        {
            _goalsToWin = goalsToWin;
        }

        public override string ToString()
        {
            return $"{_playerGoalCount}-{_enemyGoalCount}";
        }

        public void ScoreBy(CreatureSide creatureSide)
        {
            if (creatureSide == CreatureSide.Enemy)
            {
               var currentPoint= ++_enemyGoalCount;
                CheckIfWin(currentPoint,creatureSide);
            }
            else
            {
                var currentPoint= ++_playerGoalCount;
                CheckIfWin(currentPoint,creatureSide);
            }
        }

        public void ResetStats()
        {
            _playerGoalCount = default(int);
            _enemyGoalCount = default(int);
        }

        private void CheckIfWin(int score,CreatureSide creatureSide)
        {
            if (score > _goalsToWin)
            {
                Winner = creatureSide.ToString();
                EventsController.Broadcast(EventsType.EndGame);
            }
        }
    }
}