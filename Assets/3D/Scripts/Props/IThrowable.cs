using UnityEngine;

namespace Props
{
    public interface IThrowable
    {
        Transform Transform { get; set; }
        void ThrowMe(float force, Vector3 direction);
        void TakeMe(Transform placeHolder);
    }
}