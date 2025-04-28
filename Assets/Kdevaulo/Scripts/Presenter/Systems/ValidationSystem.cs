using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Presenter;

using Zenject;

namespace Kdevaulo.WordPuzzle.Service
{
    public class ValidationSystem
    {
        public event Action ValidationFailed;
        public event Action ValidationSucceed;

        [Inject]
        private WordPresenter _wordPresenter;
        [Inject]
        private SessionModel _sessionModel;

        public void ValidateWord(string solvedWord)
        {
            var words = _sessionModel.CurrentLevel.Words;

            if (words.Any(word => word.Name == solvedWord))
            {
                _sessionModel.SetSolvedWord(solvedWord);
            }
        }

        public void ValidateWords()
        {
            var level = _sessionModel.CurrentLevel;

            var cellsGroups = _wordPresenter.GetCells();

            if (!CheckGroupsCount(cellsGroups, level))
            {
                HandleFail();
                return;
            }

            var wordsCountMap = CreateWordsMap(level);

            if (!TryCountUsedWords(cellsGroups, wordsCountMap))
            {
                HandleFail();
                return;
            }

            if (!AllWordsUsed(wordsCountMap))
            {
                HandleFail();
                return;
            }

            ValidationSucceed?.Invoke();
        }

        private bool CheckGroupsCount(List<CellModel[]> cellsGroups, Level level)
        {
            return cellsGroups.Count == level.Words.Length;
        }

        private Dictionary<string, int> CreateWordsMap(Level level)
        {
            var wordsCountMap = new Dictionary<string, int>();

            foreach (var word in level.Words)
            {
                wordsCountMap.TryAdd(word.Name, 0);

                wordsCountMap[word.Name]++;
            }

            return wordsCountMap;
        }

        private bool TryCountUsedWords(List<CellModel[]> cellsGroups, Dictionary<string, int> wordsCountMap)
        {
            foreach (var group in cellsGroups)
            {
                if (!AllInCorrectState(group))
                {
                    return false;
                }

                var spelledWord = BuildWordFromCells(group);

                if (!HasExpected(wordsCountMap, spelledWord))
                {
                    return false;
                }

                wordsCountMap[spelledWord]--;
            }

            return true;
        }

        private bool HasExpected(Dictionary<string, int> wordsCountMap, string spelledWord)
        {
            return wordsCountMap.ContainsKey(spelledWord) && wordsCountMap[spelledWord] > 0;
        }

        private bool AllInCorrectState(CellModel[] group)
        {
            return group.All(c => c.CurrentState == State.Occupied && c.Cluster != null);
        }

        private bool AllWordsUsed(Dictionary<string, int> wordsCountMap)
        {
            return wordsCountMap.All(pair => pair.Value == 0);
        }

        private string BuildWordFromCells(CellModel[] cells)
        {
            var sb = new StringBuilder();
            Cluster lastCluster = null;

            foreach (var cell in cells)
            {
                var cluster = cell.Cluster;

                if (cluster != lastCluster)
                {
                    sb.Append(cluster.Name);
                    lastCluster = cluster;
                }
            }

            return sb.ToString();
        }

        private void HandleFail()
        {
            ValidationFailed?.Invoke();
        }
    }
}