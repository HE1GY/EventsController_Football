using UnityEngine;

namespace Player
{
    public class MouseLook
    {
        private const float SensativityX = 1f;
        private const float SensativityY = 0.2f;
        private const int xRotationLimite = 85;

        private float _mouseX;
        private float _mouseY;

        private float _xRotation;

        private Transform _player;
        private Transform _camera;

        public MouseLook(Transform player, Transform camera, PlayerInput playerInput)
        {
            Cursor.lockState = CursorLockMode.Locked;
            _player = player;
            _camera = camera;

            playerInput.Player.MouseX.performed += ctx =>
            {
                _mouseX = ctx.ReadValue<float>() * SensativityX;
                OnMouseX();
            };
            playerInput.Player.MouseY.performed += ctx =>
            {
                _mouseY = ctx.ReadValue<float>() * SensativityY;
                OnMouseY();
            };
        }

        private void OnMouseX()
        {
            _player.Rotate(Vector3.up * _mouseX);
        }

        private void OnMouseY()
        {
            _xRotation += -_mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -xRotationLimite, xRotationLimite);
            _camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        }
    }
}