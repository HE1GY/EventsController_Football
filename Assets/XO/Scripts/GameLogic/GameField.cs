using System.Collections;
using System.Linq;
using AI;
using UnityEngine;
using Utilities.Events;


namespace GameLogic
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private PlaceHolder[] _xoGrid;

        private Mark[] _marks;
        private Mark _currentMark = Mark.X;
        private AIBrain _opponent;
        private Mark _aiMark;
        private GameSetup _gameSetup;


        private void OnEnable()
        {
            EventsControllerXo.AddListener(EventsTypeXo.ReStart, OnRestart);
            foreach (var xo in _xoGrid)
            {
                xo.MarkChanged += OnUpdateGameField;
            }

            EventsControllerXo.AddListener<GameSetup>(EventsTypeXo.SelectMark, SetGameSetup);
        }

        private void OnDisable()
        {
            foreach (var xo in _xoGrid)
            {
                xo.MarkChanged -= OnUpdateGameField;
            }

            EventsControllerXo.RemoveListener(EventsTypeXo.ReStart, OnRestart);

            EventsControllerXo.RemoveListener<GameSetup>(EventsTypeXo.SelectMark, SetGameSetup);
        }

        private void Start()
        {
            _marks = new Mark[9];
            _opponent = new AIBrain();
        }

        private void OnUpdateGameField()
        {
            CopyPlaceHoldersMarksToMarksArray();
            if (IsWon())
            {
                EventsControllerXo.Broadcast<Mark>(EventsTypeXo.Win, _currentMark);
            }

            if (IsDraw())
            {
                EventsControllerXo.Broadcast<Mark>(EventsTypeXo.Draw, Mark.None);
            }

            ChangeCurrentMover();
            if (_aiMark == _currentMark)
            {
                AiMove();
            }
        }

        private bool IsDraw()
        {
            return _marks.All(mark => mark != Mark.None);
        }

        private void CopyPlaceHoldersMarksToMarksArray()
        {
            _marks = _xoGrid.Select(placeHolder => placeHolder.Mark).ToArray();
        }

        private bool IsWon()
        {
            return AreItemsMatch(0, 1, 2) || AreItemsMatch(3, 4, 5) || AreItemsMatch(6, 7, 8) ||
                   AreItemsMatch(0, 3, 6) || AreItemsMatch(1, 4, 7) || AreItemsMatch(2, 5, 8) ||
                   AreItemsMatch(0, 4, 8) || AreItemsMatch(2, 4, 6);
        }

        private bool AreItemsMatch(int i, int i1, int i2)
        {
            return _marks[i] == _marks[i1] && _marks[i1] == _marks[i2] && _marks[i] != Mark.None;
        }

        private void ChangeCurrentMover()
        {
            _currentMark = _currentMark switch
            {
                Mark.X => Mark.O,
                Mark.O => Mark.X,
                _ => _currentMark
            };
        }

        private void AiMove()
        {
            if (!_gameSetup.IsTwoPlayer)
            {
                CopyPlaceHoldersMarksToMarksArray();
                int aiChoice = _opponent.GetAiMoveIndex(_marks, _aiMark);
                _xoGrid[aiChoice].SetMark(_aiMark);
            }
        }

        private void OnRestart()
        {
            _currentMark = Mark.X;
            StartCoroutine(AiMoveCoroutine());
        }

        private IEnumerator AiMoveCoroutine()
        {
            yield return null; // щоб це викональ після всіx Restart, хз як правильно
            if (_aiMark == _currentMark)
            {
                AiMove();
            }
        }

        private void SetGameSetup(GameSetup gameSetup)
        {
            _gameSetup = gameSetup;
            if (!_gameSetup.IsTwoPlayer && _gameSetup.PlayerMark == Mark.O)
            {
                _aiMark = Mark.X;
                AiMove();
            }
            else
            {
                _aiMark = Mark.O;
            }
        }
    }
}