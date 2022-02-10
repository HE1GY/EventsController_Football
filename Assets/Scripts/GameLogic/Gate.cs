using Architecture.Stats;
using Core;
using UnityEngine;

namespace GameLogic
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private CreatureSide _whomGate;
        private void OnTriggerEnter(Collider other)
        {
            if (_whomGate != CreatureSide.Player)
            {
                PlayerScore();
            }
            else
            {
                EnemyScore();
            }
        }
        private void PlayerScore() => EventsController.Broadcast(EventsType.PlayerScoreGoal);

        private void EnemyScore() => EventsController.Broadcast(EventsType.EnemyScoreGoal);
    }
}
