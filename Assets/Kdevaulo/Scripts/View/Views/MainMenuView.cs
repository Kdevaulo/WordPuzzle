using System;

using Kdevaulo.WordPuzzle.Core;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(MainMenuView) + " in " + nameof(View))]
    public class MainMenuView : MonoBehaviour, IMainMenuView, IDisposable
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _playButton;

        private IMainMenuPresenter _presenter;

        [Inject]
        public void Construct(IMainMenuPresenter presenter)
        {
            _presenter = presenter;

            _settingsButton.onClick.AddListener(HandleSettingsClick);
            _playButton.onClick.AddListener(HandlePlayClick);
        }

        void IDisposable.Dispose()
        {
            _settingsButton.onClick.RemoveListener(HandleSettingsClick);
            _playButton.onClick.RemoveListener(HandlePlayClick);
        }

        void IMainMenuView.DisableButtons()
        {
            _settingsButton.interactable = false;
            _playButton.interactable = false;
        }

        private void HandlePlayClick()
        {
            _presenter.HandlePlayClick();
        }

        private void HandleSettingsClick()
        {
            _presenter.HandleSettingsClick();
        }
    }
}