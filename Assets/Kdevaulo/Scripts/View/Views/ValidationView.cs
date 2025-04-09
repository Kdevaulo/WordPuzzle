using System;

using Kdevaulo.WordPuzzle.Core;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Kdevaulo.WordPuzzle.View
{
    [AddComponentMenu(nameof(ValidationView) + " in " + nameof(View))]
    public class ValidationView : MonoBehaviour, IDisposable, IValidationView
    {
        [SerializeField] private Image _image;

        [SerializeField] private Button _button;

        private IValidationPresenter _presenter;

        [Inject]
        public void Construct(IValidationPresenter presenter)
        {
            _presenter = presenter;

            _button.onClick.AddListener(HandleValidateClick);
        }

        void IDisposable.Dispose()
        {
            if (_button)
            {
                _button.onClick.RemoveListener(HandleValidateClick);
            }
        }

        void IValidationView.HandleSuccess()
        {
            _image.color = Color.green;
        }

        void IValidationView.HandleFail()
        {
            _image.color = Color.red;
        }

        private void HandleValidateClick()
        {
            _presenter.Validate();
        }
    }
}