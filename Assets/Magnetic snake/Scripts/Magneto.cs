using System;
using UnityEngine;

public class Magneto : MonoBehaviour
{
    public event Action<bool, Vector3, Rigidbody> CanCatch;
    public event Action<Vector3> SteerSnake;

    [SerializeField] private Transform _snakeHead;
    [SerializeField] private float _power = 1;
    [SerializeField] private Transform _camera;

    private const int MaxDistance = 1000;
    private const int CatchDistance = 2;
    private const string LayerNamesMagnetic = "Magnetic";
    private const string LayerNamesTower = "Tower";

    private bool _isOnSnake;


    private void Update()
    {
        if (_isOnSnake)
        {
            TowerMagneto();
        }
        else
        {
            CommonMagneto();
        }
    }

    public void SetIsOnSnake(bool isOnSnake)
    {
        _isOnSnake = isOnSnake;
    }

    private void CommonMagneto()
    {
        Vector3 rayOrg = _camera.position;
        Vector3 rayDir = _camera.forward;
        Ray ray = new Ray(rayOrg, rayDir);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance, LayerMask.GetMask(LayerNamesMagnetic)))
        {
            float distance = Vector3.Distance(transform.position, raycastHit.point);
            if (distance < CatchDistance)
            {
                CanCatch?.Invoke(true, raycastHit.point, raycastHit.rigidbody);
            }
            else
            {
                raycastHit.rigidbody.AddForce((transform.position - raycastHit.point) * _power);
                CanCatch?.Invoke(false, raycastHit.point, raycastHit.rigidbody);
            }
        }
        else
        {
            CanCatch?.Invoke(false, raycastHit.point, raycastHit.rigidbody);
        }
    }

    private void TowerMagneto()
    {
        Vector3 rayOrg = _camera.position;
        Vector3 rayDir = _camera.forward;
        Ray ray = new Ray(rayOrg, rayDir);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, MaxDistance, LayerMask.GetMask(LayerNamesTower)))
        {
            Vector3 formSnakeToTower = raycastHit.point - _snakeHead.position;
            SteerSnake?.Invoke(formSnakeToTower);
        }
    }
}