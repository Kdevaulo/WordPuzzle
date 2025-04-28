using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presenter
{
    public class ValidationPresenter : IInitializable, IDisposable
    {
        [Inject]
        private ValidationSystem _validationSystem;
        [Inject]
        private SceneChangeSystem _sceneSystem;
        [Inject]
        private ValidationView _view;

        private CancellationTokenSource _cts;

        void IInitializable.Initialize()
        {
            _cts = new CancellationTokenSource();

            _validationSystem.ValidationSucceed += HandleValidationSucceed;
            _validationSystem.ValidationFailed += HandleValidationFailed;
            _view.OnValidateClick += Validate;
        }

        void IDisposable.Dispose()
        {
            _validationSystem.ValidationSucceed -= HandleValidationSucceed;
            _validationSystem.ValidationFailed -= HandleValidationFailed;
            _view.OnValidateClick -= Validate;
        }

        private void Validate()
        {
            _validationSystem.ValidateWords();
        }

        private void HandleValidationFailed()
        {
            _view.HandleFail();
        }

        private void HandleValidationSucceed()
        {
            _view.HandleSuccess();

            _sceneSystem.SwitchSceneAsync(SceneType.Victory, true, _cts.Token).Forget();
        }
    }
}