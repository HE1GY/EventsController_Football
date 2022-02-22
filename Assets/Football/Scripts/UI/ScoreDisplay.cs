using Architecture.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private StatsManager _statsManager;

        private void OnEnable()
        {
            _statsManager.ScoreChange += UpdateStats;
        }

        private void OnDisable()
        {
            _statsManager.ScoreChange -= UpdateStats;
        }

        public void UpdateStats(string score)
        {
            _scoreText.text = score;
        }
    }
}