using System;

using Kdevaulo.WordPuzzle.Core;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ValidationPresenter : IInitializable, IValidationPresenter, IDisposable
    {
        [Inject]
        private IValidationService _service;
        [Inject]
        private IValidationView _view;
        [Inject]
        private ISessionModel _model;

        void IInitializable.Initialize()
        {
            _service.ValidationSucceed += HandleValidationSucceed;
            _service.ValidationFailed += HandleValidationFailed;
        }

        void IValidationPresenter.Validate()
        {
            var level = _model.TryGetLevel();
            _service.ValidateWords(level);
        }

        void IDisposable.Dispose()
        {
            _service.ValidationSucceed -= HandleValidationSucceed;
            _service.ValidationFailed -= HandleValidationFailed;
        }

        private void HandleValidationFailed()
        {
            _view.HandleFail();
        }

        private void HandleValidationSucceed()
        {
            _view.HandleSuccess();
        }
    }
}