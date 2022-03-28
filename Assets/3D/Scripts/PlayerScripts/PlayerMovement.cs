using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


namespace PlayerScripts
{
    public class PlayerMovement
    {
        public event Action Grounded;

        private const float Acceleration = 0.05f;
        private const float Deceleration = 0.05f;
        private const float AirDeceleration = 0.02f;
        private const float TurnSpeed = 5;
        private const float Gravity = -9.8f;
        private const float SphereGroundCheckRadius = 0.2f;
        private const int GroundedGravity = -4;

        private readonly CharacterController _characterController;
        private readonly Transform _camTransform;
        private readonly LayerMask _groundLayerMask;

        private readonly float _runSpeed;
        private readonly float _walkSpeed;
        private readonly float _jumpHeight;

        private float _currentSpeed;
        private float _verticalVelocity;

        private float _horizontalInput;
        private float _verticalInput;

        private Vector3 _moveDirection;

        private bool _isRunPress;
        private bool _isWalkPress;
        private bool _isGrounded;
        private bool _isMovable;


        public PlayerMovement(CharacterController characterController, PlayerInput3D input, MoveValueSetup moveValueSetup,
            Transform camTransform, LayerMask groundLayerMask)
        {
            _characterController = characterController;
            _walkSpeed = moveValueSetup.WalkSpeed;
            _runSpeed = moveValueSetup.RunSpeed;
            _jumpHeight = moveValueSetup.JumpHeight;
            _camTransform = camTransform;
            _groundLayerMask = groundLayerMask;

            input.Player.Walk.performed += OnInputDirection;
            input.Player.Walk.canceled += _ => _isWalkPress = false;

            input.Player.Run.started += OnInputRun;
            input.Player.Run.canceled += OnInputRun;

            input.Player.Jump.performed += ctx => Jump();
        }


        public void HandleHorizontalMove(bool isMovable)
        {
            _isMovable = isMovable;
            _moveDirection = GetMoveDirection();
            if (isMovable && _isWalkPress && _currentSpeed <= _walkSpeed)
            {
                _currentSpeed += Acceleration;
                _currentSpeed = RoundValue(_currentSpeed, _walkSpeed, true);
            }
            else if (!_isWalkPress)
            {
                _currentSpeed -= Deceleration;
                _currentSpeed = RoundValue(_currentSpeed, 0, false);
            }
            else if (!isMovable)
            {
                _currentSpeed -= AirDeceleration;
                _currentSpeed = RoundValue(_currentSpeed, 0, false);
            }

            if (isMovable && _isRunPress && _isWalkPress && _currentSpeed <= _runSpeed)
            {
                _currentSpeed += Acceleration;
                _currentSpeed = RoundValue(_currentSpeed, _runSpeed, true);
            }
            else if (!_isRunPress && _currentSpeed > _walkSpeed)
            {
                _currentSpeed -= Deceleration;
                _currentSpeed = RoundValue(_currentSpeed, _walkSpeed, false);
            }
            else if (!isMovable && _currentSpeed > _walkSpeed)
            {
                _currentSpeed -= AirDeceleration;
                _currentSpeed = RoundValue(_currentSpeed, _walkSpeed, false);
            }

            _characterController.Move(_moveDirection * _currentSpeed * Time.deltaTime);
        }

        public void HandleRotation()
        {
            if (_isWalkPress)
            {
                Vector3 targetDirection = GetMoveDirection();
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                Quaternion newRotation = Quaternion.Slerp(_characterController.transform.rotation, targetRotation,
                    TurnSpeed * Time.deltaTime);
                _characterController.transform.rotation = newRotation;
            }
        }

        public void HandleVerticalMove()
        {
            Vector3 position = _characterController.transform.position;
            bool isGrounded = Physics.CheckSphere(position, SphereGroundCheckRadius, _groundLayerMask);
            HandleGravity(isGrounded);
            _characterController.Move(Vector3.up * _verticalVelocity * Time.deltaTime);
        }

        public float GetNormalizedHorizontalVelocity() => _currentSpeed / _runSpeed;
        public float GetVerticalVelocity() => _verticalVelocity;

        public float GetVelocity() => _currentSpeed;


        private void Jump()
        {
            if (_isGrounded && _isMovable)
            {
                float jumpVelocity = Mathf.Sqrt(_jumpHeight * -2 * Gravity);
                _verticalVelocity = jumpVelocity;
            }
        }


        private void HandleGravity(bool isGrounded)
        {
            if (!isGrounded)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
                _isGrounded = false;
            }
            else
            {
                if (_isGrounded != true)
                {
                    _isGrounded = true;
                    Grounded?.Invoke();
                    _verticalVelocity = GroundedGravity;
                }
            }
        }

        private void OnInputDirection(InputAction.CallbackContext ctx)
        {
            Vector2 inputVector = ctx.ReadValue<Vector2>();
            _isWalkPress = inputVector.x != 0 || inputVector.y != 0;
            _horizontalInput = inputVector.x;
            _verticalInput = inputVector.y;
        }

        private void OnInputRun(InputAction.CallbackContext ctx) => _isRunPress = ctx.ReadValueAsButton();

        private Vector3 GetMoveDirection()
        {
            Vector3 direction = _verticalInput * _camTransform.forward;
            direction += _camTransform.right * _horizontalInput;
            direction.Normalize();
            direction.y = 0;
            return direction;
        }

        private float RoundValue(float value, float toValue, bool isLess)
        {
            float error = 0.05f;
            if (isLess && toValue - error < value)
            {
                value = toValue;
            }
            else if (!isLess && toValue + error > value)
            {
                value = toValue;
            }

            return value;
        }
    }
}