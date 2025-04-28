using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using UnityEngine;
using UnityEngine.Assertions;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class GameController : IInitializable, IDisposable
    {
        [Inject]
        private LevelLoadingSystem _levelLoader;
        [Inject]
        private ClusterSpawner _clusterSpawner;
        [Inject]
        private SessionModel _model;
        [Inject]
        private ValidationSystem _validationSystem;

        private const string CurrentLevelKey = "CurrentLevelKey";

        private CancellationTokenSource _cts;

        private int _currentLevel;

        void IInitializable.Initialize()
        {
            _validationSystem.ValidationSucceed += IncreaseLevel;

            _cts = new CancellationTokenSource();
            TryStartLevelAsync(_cts.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            _validationSystem.ValidationSucceed -= IncreaseLevel;
        }

        private void IncreaseLevel()
        {
            ++_currentLevel;
            PlayerPrefs.SetInt(CurrentLevelKey, _currentLevel);
        }

        private async UniTask TryStartLevelAsync(CancellationToken token)
        {
            _currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);

            if (_currentLevel < 0 || _currentLevel > _levelLoader.GetLevelsCount())
            {
                _currentLevel = 1;
            }

            await _levelLoader.LoadLevelAsync(_currentLevel, token);

            var loadedLevel = _levelLoader.GetLoadedLevel();
            Assert.IsNotNull(loadedLevel);

            _model.CurrentLevel = loadedLevel;

            if (loadedLevel?.Words != null && loadedLevel.Words.Length != 0)
            {
                _clusterSpawner.CreateClusters(loadedLevel);
            }
        }
    }
}