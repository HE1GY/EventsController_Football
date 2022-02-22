using UnityEngine;

namespace GameLogic.Creature
{
    public abstract class CreatureMove : MonoBehaviour
    {
        private const float ZMaxBound = 3;
        private const float ZMinBound = -3;

        [SerializeField] protected float _speed;

        protected Vector3 GetClampedZVector()
        {
            var position = transform.position;
            var zClamped = Mathf.Clamp(position.z, ZMinBound, ZMaxBound);
            return new Vector3(position.x, position.y, zClamped);
        }

        protected abstract void Move();
    }
}