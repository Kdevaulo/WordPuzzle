using System;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ValidationView : MonoBehaviour, IDisposable
    {
        public event Action OnValidateClick;

        [SerializeField] private Image _image;

        [SerializeField] private Button _button;

        [Inject]
        public void Construct()
        {
            _button.onClick.AddListener(HandleValidateClick);
        }

        void IDisposable.Dispose()
        {
            if (_button)
            {
                _button.onClick.RemoveListener(HandleValidateClick);
            }
        }

        public void HandleSuccess()
        {
            _image.color = Color.green;
        }

        public void HandleFail()
        {
            _image.color = Color.red;
        }

        private void HandleValidateClick()
        {
            OnValidateClick?.Invoke();
        }
    }
}