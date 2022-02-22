using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic;


namespace AI
{
    public class AIBrain
    {
        private Mark _currentMark;

        public int GetAiMoveIndex(Mark[] marks, Mark aiMark)
        {
            _currentMark = aiMark;
            return MinMax(marks, aiMark, 0);
        }

        private int MinMax(Mark[] marks, Mark playerMark, int layer)
        {
            if (IsWon(marks, _currentMark))
            {
                return 10 + layer;
            }

            if (IsWon(marks, GetOppositeMark(_currentMark)))
            {
                return -10 - layer;
            }

            if (IsDraw(marks))
            {
                return 0;
            }

            if (playerMark == _currentMark)
            {
                int bestScore = Int32.MinValue;
                int bestIndex = -1;
                List<int> emptyCellIndexs = GetEmptyCellIndex(marks);
                for (int i = 0; i < emptyCellIndexs.Count; i++)
                {
                    Mark[] copyMarks = marks.ToArray();
                    copyMarks[emptyCellIndexs[i]] = _currentMark;

                    int branchScore = MinMax(copyMarks, GetOppositeMark(_currentMark), layer - 1);

                    if (branchScore > bestScore)
                    {
                        bestIndex = emptyCellIndexs[i];
                        bestScore = branchScore;
                    }
                }

                if (layer == 0)
                {
                    return bestIndex;
                }

                return bestScore;
            }
            else
            {
                int worseScore = Int32.MaxValue;
                List<int> emptyCellIndexs = GetEmptyCellIndex(marks);
                for (int i = 0; i < emptyCellIndexs.Count; i++)
                {
                    Mark[] copyMarks = marks.ToArray();
                    copyMarks[emptyCellIndexs[i]] = GetOppositeMark(_currentMark);

                    int branchScore = MinMax(copyMarks, GetOppositeMark(playerMark), layer - 1);

                    if (branchScore < worseScore)
                    {
                        worseScore = branchScore;
                    }
                }

                return worseScore;
            }
        }

        private bool IsWon(Mark[] marks, Mark playerMark)
        {
            return AreMarksMatch(0, 1, 2, playerMark, marks) || AreMarksMatch(3, 4, 5, playerMark, marks) ||
                   AreMarksMatch(6, 7, 8, playerMark, marks) ||
                   AreMarksMatch(0, 3, 6, playerMark, marks) || AreMarksMatch(1, 4, 7, playerMark, marks) ||
                   AreMarksMatch(2, 5, 8, playerMark, marks) ||
                   AreMarksMatch(0, 4, 8, playerMark, marks) || AreMarksMatch(2, 4, 6, playerMark, marks);
        }

        private bool AreMarksMatch(int i, int i1, int i2, Mark sameMark, Mark[] marksField)
        {
            return marksField[i] == marksField[i1] && marksField[i2] == marksField[i1] && marksField[i2] == sameMark;
        }

        private bool IsDraw(Mark[] marks)
        {
            return marks.All(m => m != Mark.None);
        }

        private List<int> GetEmptyCellIndex(Mark[] marksField)
        {
            List<int> emptyCellIndex = new List<int>();
            for (int i = 0; i < marksField.Length; i++)
            {
                if (marksField[i] == Mark.None)
                {
                    emptyCellIndex.Add(i);
                }
            }

            return emptyCellIndex;
        }

        private Mark GetOppositeMark(Mark currentMark) => currentMark == Mark.X ? Mark.O : Mark.X;
    }
}