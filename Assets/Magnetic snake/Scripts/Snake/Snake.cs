using UnityEngine;

namespace Snake
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _bodySegment;
        [SerializeField] private GrowZone _growZone;
        [SerializeField] private Magneto _magneto;
        [SerializeField] private float _boomForce;

        private SnakeMover _snakeMover;
        private Rigidbody _rigidbody;
        private PlayerInput _input;

        private Body _body;

        private bool _dead;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = new PlayerInput();

            _snakeMover = new SnakeMover(_rigidbody, _speed);
            _body = new Body(_bodySegment, _growZone);
            _magneto.SteerSnake += _snakeMover.SetLookDirection;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            if (!_dead)
            {
                _snakeMover.HandleMovement();
                _snakeMover.HandleRotation();
                _body.ScanMovePosition(transform.position);
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
            {
                _dead = true;
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(Vector3.up * _boomForce, ForceMode.Acceleration);
            }
        }
    }
}