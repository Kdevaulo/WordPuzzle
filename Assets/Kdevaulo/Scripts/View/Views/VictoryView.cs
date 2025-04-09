using System;
using System.Text;

using Kdevaulo.WordPuzzle.Core;

using TMPro;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using Zenject;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(VictoryView) + " in " + nameof(View))]
    public class VictoryView : MonoBehaviour, IDisposable, IVictoryView
    {
        [SerializeField] private TextMeshProUGUI _resultTextContainer;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _nextButton;

        private IVictoryPresenter _presenter;

        [Inject]
        public void Construct(IVictoryPresenter presenter)
        {
            _presenter = presenter;

            _menuButton.onClick.AddListener(HandleMenuClick);
            _nextButton.onClick.AddListener(HandleNextClick);
        }

        void IDisposable.Dispose()
        {
            _menuButton.onClick.RemoveListener(HandleMenuClick);
            _nextButton.onClick.RemoveListener(HandleNextClick);
        }

        void IVictoryView.SetText(string[] words)
        {
            Assert.IsNotNull(words);

            var sb = new StringBuilder();

            foreach (var word in words)
            {
                sb.AppendLine(word);
            }

            _resultTextContainer.text = sb.ToString();
        }

        private void HandleNextClick()
        {
            _presenter.HandleNextClick();
        }

        private void HandleMenuClick()
        {
            _presenter.HandleMenuClick();
        }
    }
}