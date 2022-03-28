using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Body
    {
        private const int Gab = 300;

        private List<Vector3> _moveHistory = new List<Vector3>();
        private List<Segment> _bodySegments = new List<Segment>();

        private GrowZone _growZone;

        private GameObject _bodyPartTemplate;

        public Body(GameObject bodyPartTemplate, GrowZone growZone)
        {
            _bodyPartTemplate = bodyPartTemplate;
            _growZone = growZone;
            _growZone.Grow += GrowSnake;
            GrowSnake();
            GrowSnake();
            GrowSnake();
        }

        private void GrowSnake()
        {
            GameObject bodyPart = GameObject.Instantiate(_bodyPartTemplate);
            Segment bodySegment = bodyPart.GetComponent<Segment>();
            _bodySegments.Add(bodySegment);
        }

        public void ScanMovePosition(Vector3 position)
        {
            _moveHistory.Insert(0, position);

            for (int i = 0; i < _bodySegments.Count; i++)
            {
                Vector3 point = _moveHistory[Mathf.Min(i * Gab, _moveHistory.Count - 1)];
                _bodySegments[i].HandleSegmentMove(point);
            }

            Vector3 growPos = _moveHistory[Mathf.Min(_bodySegments.Count * Gab, _moveHistory.Count - 1)];
            _growZone.transform.LookAt(growPos);
            _growZone.transform.position = growPos;
        }
    }
}