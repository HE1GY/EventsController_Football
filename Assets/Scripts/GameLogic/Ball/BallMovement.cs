using Core;
using GameLogic.Creature;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace GameLogic.Ball
{
    public class BallMovement : MonoBehaviour
    {
        public Vector3 Direction { private set; get; }

        [SerializeField] private float _speed;

        private Vector3 _startPosition;
        private bool _isGamePlaying;

        private void OnEnable()
        {
            EventsController.AddListener(EventsType.EnemyScoreGoal,ResetPosition);
            EventsController.AddListener(EventsType.PlayerScoreGoal,ResetPosition);
            EventsController.AddListener(EventsType.EndGame,OnPause);
            EventsController.AddListener(EventsType.PauseGame,OnPause);
            EventsController.AddListener(EventsType.ResetGame,ResetPosition);
            EventsController.AddListener(EventsType.PlayGame,OnPlaying);

        }
        
        private void OnDisable()
        {
            EventsController.RemoveListener(EventsType.PlayerScoreGoal,ResetPosition);
            EventsController.RemoveListener(EventsType.EnemyScoreGoal,ResetPosition);
            EventsController.RemoveListener(EventsType.EndGame,OnPause);
            EventsController.RemoveListener(EventsType.PauseGame,OnPause);
            EventsController.RemoveListener(EventsType.ResetGame,ResetPosition);
            EventsController.RemoveListener(EventsType.PlayGame,OnPlaying);
        }

        private void Start()
        {
            _startPosition = transform.position;
            Direction = GetRandomDirection();
        }

        private void Update()
        {
            if (_isGamePlaying)
            {
                Move();
            }
        }


        private void ResetPosition()
        {
            transform.position = _startPosition;
            Direction = GetRandomDirection();
        }

        private void OnPause() => _isGamePlaying = false;

        private void OnPlaying() => _isGamePlaying = true;


        private void OnTriggerEnter(Collider other)
        {
            ChangeDirection(other.transform.forward);
            if (other.TryGetComponent(out CreatureMove creature))
            {
                SlightChangeDirection();
            }
        }

        private void SlightChangeDirection()
        {
            var сoefficientReflection = Random.Range(0.7f, 1f);
            Direction  *= сoefficientReflection;
        }

        private void Move()
        {
            transform.Translate(Direction.normalized * _speed * Time.deltaTime);
        }

        private void ChangeDirection(Vector3 surfaceNormal)
        {
            Direction = Vector3.Reflect(Direction, surfaceNormal);
        }

        private Vector3 GetRandomDirection()
        {
            var randomZ = Random.Range(-7, 7);
            var randomX = Random.Range(-7, 7);
            if (randomX != 0 && randomZ != 1)
            {
                return GetRandomDirection();
            }
            else if (randomX != 1 && randomZ != 0)
            {
                return GetRandomDirection();
            }
            var newDirection = new Vector3(randomX, 0, randomZ);
            if (newDirection != Vector3.zero)
            {
                return newDirection;
            }
            else
            {
                return GetRandomDirection();
            }
        }
    }
}