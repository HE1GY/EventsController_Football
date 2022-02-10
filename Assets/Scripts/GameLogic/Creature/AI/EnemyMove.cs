using GameLogic.Ball;
using UnityEngine;

namespace GameLogic.Creature.AI
{
    public class EnemyMove : CreatureMove
    {
        [SerializeField] private BallMovement _ballMovement;

        private void Update()
        {
            if (_ballMovement.Direction.x > 0)
            {
                Defence();
            }
        }

        private void Defence()
        {
            Move();
            transform.position=base.GetClampedZVector();
        }
        
        protected override void Move()
        {
            var speedPerSec = _speed * Time.deltaTime;
            if (_ballMovement.transform.position.z > transform.position.z)
            {
                transform.Translate(-transform.forward*speedPerSec);
            }
            else if(_ballMovement.transform.position.z < transform.position.z)
            {
                transform.Translate(transform.forward*speedPerSec);
            }
        }
    }
}
