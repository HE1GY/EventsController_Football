using System.Threading.Tasks;
using Snake;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private MagneticSegment _magneticSegmentPrefabs;
    [SerializeField] private float _timeToSpawn;


    private void Start()
    {
        Spawn();
    }

    private async void Spawn()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, _points.Length - 1);
            Instantiate(_magneticSegmentPrefabs, _points[randomIndex].position, Quaternion.identity);
            await Task.Delay((int) _timeToSpawn * 1000);
        }
    }
}