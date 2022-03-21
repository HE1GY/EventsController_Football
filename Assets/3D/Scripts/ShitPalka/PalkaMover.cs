using System.Collections;
using UnityEngine;

namespace ShitPalka
{
    public class PalkaMover
    {
        private const float RecoverTime = 2;
        private const int OnHitSpeed = 0;

        private readonly CharacterController _characterController;
        private readonly MonoBehaviour _coroutineRunner;
        private readonly float _defaultSpeed;
        private float _currentSpeed;


        public PalkaMover(CharacterController characterController, float defaultSpeed, MonoBehaviour coroutineRunner)
        {
            _characterController = characterController;
            _defaultSpeed = defaultSpeed;
            _currentSpeed = _defaultSpeed;
            _coroutineRunner = coroutineRunner;
        }


        public void HandleMovement()
        {
            _characterController.SimpleMove(_characterController.transform.forward * _currentSpeed);
        }

        public void OnHit()
        {
            _currentSpeed = OnHitSpeed;
            _coroutineRunner.StartCoroutine(Recover());
        }


        private IEnumerator Recover()
        {
            yield return new WaitForSeconds(RecoverTime);
            _currentSpeed = _defaultSpeed;
        }
    }
}