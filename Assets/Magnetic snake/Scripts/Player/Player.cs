using UnityEngine;
using UnityEngine.Animations.Rigging;


namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private LandScope _landScope;
        [SerializeField] private Magneto _magneto;
        [SerializeField] private Rig _handRig;
        [SerializeField] private Transform _placeHolder;
        [SerializeField] private Animator _handAnimator;

        private MouseLook _mouseLook;
        private PlayerInput _playerInput;
        private PlayerMove _playerMove;
        private LandScopePlacer _landScopePlacer;
        private PlayerIk _playerIk;
        private PlayerCatcher _playerCatcher;
        private LegAnimation _legAnimation;
        private GroundChecker _groundChecker;

        private Rigidbody _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _playerInput = new PlayerInput();
            _mouseLook = new MouseLook(transform, _camera, _playerInput);
            _landScopePlacer = new LandScopePlacer(_camera, _landScope, _playerInput);
            _playerMove = new PlayerMove(_rigidbody);
            _playerIk = new PlayerIk(_handRig);
            _playerCatcher = new PlayerCatcher(_placeHolder, _playerInput);
            _legAnimation = new LegAnimation(_handAnimator);
            _groundChecker = new GroundChecker(transform);


            _magneto.CanCatch += _playerCatcher.OnCanCatch;
            _playerCatcher.Catch += _playerIk.OnCatch;
            _playerIk.Catch += _legAnimation.PlayCatch;
            _landScopePlacer.Jump += _playerMove.Jump;
        }


        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Update()
        {
            _magneto.SetIsOnSnake(_groundChecker.IsOnSnake);
            _landScopePlacer.HandleLandScopePlacing();
            _groundChecker.HandleGroundChecking();
        }
    }
}