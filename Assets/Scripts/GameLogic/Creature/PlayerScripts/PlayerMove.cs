using UnityEngine;

namespace GameLogic.Creature.PlayerScripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMove:CreatureMove
    {

        private PlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            Move();
            transform.position=base.GetClampedZVector();
        }

        protected override  void Move()
        {
            transform.Translate(-transform.forward* _input.InputZ*_speed*Time.deltaTime);
        }

        
    }
}