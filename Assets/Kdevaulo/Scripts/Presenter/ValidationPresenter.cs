using System;
using System.Threading;

using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Core.Data;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ValidationPresenter : IInitializable, IValidationPresenter, IDisposable
    {
        [Inject]
        private IValidationService _validationService;
        [Inject]
        private ISceneService _sceneService;
        [Inject]
        private IValidationView _view;
        [Inject]
        private ISessionModel _model;

        private CancellationTokenSource _cts;

        void IInitializable.Initialize()
        {
            _cts = new CancellationTokenSource();

            _validationService.ValidationSucceed += HandleValidationSucceed;
            _validationService.ValidationFailed += HandleValidationFailed;
        }

        void IValidationPresenter.Validate()
        {
            var level = _model.TryGetLevel();
            _validationService.ValidateWords(level);
        }

        void IDisposable.Dispose()
        {
            _validationService.ValidationSucceed -= HandleValidationSucceed;
            _validationService.ValidationFailed -= HandleValidationFailed;
        }

        private void HandleValidationFailed()
        {
            _view.HandleFail();
        }

        private void HandleValidationSucceed()
        {
            _view.HandleSuccess();
            _sceneService.SwitchSceneAsync(SceneType.Victory, true, _cts.Token);
        }
    }
}