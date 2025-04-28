using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class MainMenuView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _playButton;

        private SceneChangeSystem _sceneChangeSystem;
        private CancellationTokenSource _cts;

        [Inject]
        public void Construct(SceneChangeSystem sceneSystem)
        {
            _sceneChangeSystem = sceneSystem;
            _cts = new CancellationTokenSource();

            _settingsButton.onClick.AddListener(HandleSettingsClick);
            _playButton.onClick.AddListener(OnPlayClick);
        }

        void IDisposable.Dispose()
        {
            _settingsButton.onClick.RemoveListener(HandleSettingsClick);
            _playButton.onClick.RemoveListener(OnPlayClick);
        }

        private void OnPlayClick()
        {
            DisableButtons();
            _sceneChangeSystem.SwitchSceneAsync(SceneType.Game, true, _cts.Token).Forget();
        }

        private void HandleSettingsClick()
        {
            DisableButtons();
            _sceneChangeSystem.SwitchSceneAsync(SceneType.Settings, true, _cts.Token).Forget();
        }

        private void DisableButtons()
        {
            _settingsButton.interactable = false;
            _playButton.interactable = false;
        }
    }
}