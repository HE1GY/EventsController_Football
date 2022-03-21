using System;
using UnityEngine;

namespace ShitPalka
{
    public class Palka : MonoBehaviour
    {
        [Header("Leg1")] [SerializeField] private Transform _ikTargetTransform1;
        [SerializeField] private Transform _rayOrg1;
        [SerializeField] private Animator _animator1;
        [SerializeField] private float _distanceToMoveLeg1;

        [Header("Leg2")] [SerializeField] private Transform _ikTargetTransform2;
        [SerializeField] private Transform _rayOrg2;
        [SerializeField] private Animator _animator2;
        [SerializeField] private float _distanceToMoveLeg2;

        [Header("Leg3")] [SerializeField] private Transform _ikTargetTransform3;
        [SerializeField] private Transform _rayOrg3;
        [SerializeField] private Animator _animator3;
        [SerializeField] private float _distanceToMoveLeg3;

        [Header("Leg4")] [SerializeField] private Transform _ikTargetTransform4;
        [SerializeField] private Transform _rayOrg4;
        [SerializeField] private Animator _animator4;
        [SerializeField] private float _distanceToMoveLeg4;

        [Header("Movement")]
        [SerializeField] private float _speed;

        private Leg _leg1;
        private Leg _leg2;
        private Leg _leg3;
        private Leg _leg4;

        private PalkaMover _palkaMover;
        private PalkaCollision _palkaCollision;
        private CharacterController _characterController;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            _palkaMover = new PalkaMover(_characterController, _speed, this);
            _palkaCollision = new PalkaCollision();

            _leg1 = new Leg(_ikTargetTransform1, _rayOrg1, _animator1, _distanceToMoveLeg1);
            _leg2 = new Leg(_ikTargetTransform2, _rayOrg2, _animator2, _distanceToMoveLeg2);
            _leg3 = new Leg(_ikTargetTransform3, _rayOrg3, _animator3, _distanceToMoveLeg3);
            _leg4 = new Leg(_ikTargetTransform4, _rayOrg4, _animator4, _distanceToMoveLeg4);

            _palkaCollision.Hit += _palkaMover.OnHit;
        }

        public void SubscribeOnHit(Action methods)
        {
            _palkaCollision.Hit += methods;
        }

        public void UnSubscribeOnHit(Action methods)
        {
            _palkaCollision.Hit -= methods;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _palkaCollision.HandleCollision(hit);
        }

        private void Update()
        {
            _leg1.HandleMovement();
            _leg2.HandleMovement();
            _leg3.HandleMovement();
            _leg4.HandleMovement();

            _palkaMover.HandleMovement();
        }
    }
}