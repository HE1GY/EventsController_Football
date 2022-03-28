using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public event Action MakeStep;

        [Header("MoveSetup")] [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Transform _camTransform;

        [Header("Gravity")] [SerializeField] private LayerMask _groundMask;

        [Header("Take&Throw")] [SerializeField]
        private Transform _placeHolder;

        [SerializeField] private LayerMask _itemMask;
        [SerializeField] private float _throwForce;
        [SerializeField] private float _takeDistance;

        [Header("IK")] [SerializeField] private Rig _rArmRig;

        private Animator _animator;
        private CharacterController _characterController;

        private PlayerInput3D _playerInput;
        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private TakeThrower _takeThrower;
        private PlayerIK _playerIK;

        private bool _isInteractable = true;


        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();

            MoveValueSetup moveValueSetup = new MoveValueSetup(_walkSpeed, _runSpeed, _jumpHeight);

            _playerInput = new PlayerInput3D();
            _playerMovement = new PlayerMovement(_characterController, _playerInput, moveValueSetup, _camTransform,
                _groundMask);
            _playerAnimation = new PlayerAnimation(_animator);
            _takeThrower = new TakeThrower(_placeHolder, _camTransform, _itemMask, _playerInput, _throwForce,
                _takeDistance);
            _playerIK = new PlayerIK(_rArmRig);


            _playerMovement.Grounded += _playerAnimation.SetLandedTrigger;
            _takeThrower.TakeItem += OnTaking;
            _takeThrower.ThrowItem += OnThrowing;
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
            _playerMovement.HandleHorizontalMove(_isInteractable);
            _playerMovement.HandleRotation();
            _playerMovement.HandleVerticalMove();
            _playerAnimation.SetHorizontalVelocity(_playerMovement.GetNormalizedHorizontalVelocity());
            _playerAnimation.SetVerticalVelocity(_playerMovement.GetVerticalVelocity());
        }

        public float GetSpeed() => _playerMovement.GetVelocity();

        public void SubscribeOnCanTake(Action<bool> method)
        {
            _takeThrower.CanTake += method;
        }

        public void UnSubscribeOnCanTake(Action<bool> method)
        {
            _takeThrower.CanTake -= method;
        }

        private void OnTaking(Vector3 targetPos)
        {
            _playerAnimation.PlayTaking();
            _playerIK.SetActiveArmRig();
            _playerIK.SetIKArmTarget(targetPos);
        }

        private void OnThrowing()
        {
            _playerAnimation.PlayThrowing();
        }


        #region Animation Methods

        private void ToMakeStepsInWalkRunAnimation()
        {
            MakeStep?.Invoke();
        }

        private void SetInteractableFalse()
        {
            _isInteractable = false;
        }

        private void SetInteractableTrue()
        {
            _isInteractable = true;
        }

        private void InTakeAnimation()
        {
            _takeThrower.FinallyTake();
            StartCoroutine(BagFix());
            SetInteractableTrue();
        }

        private void InThrowAnimation()
        {
            _takeThrower.FinallyThrow();
        }

        private void InTheEndThrowAnimation()
        {
            _playerAnimation.BackFromThrowingLayer();
        }

        #endregion

        private IEnumerator BagFix()
        {
            yield return new WaitForEndOfFrame();
            _playerIK.SetUnActiveArmRig();
        }
    }
}