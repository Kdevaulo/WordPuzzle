using Kdevaulo.WordPuzzle.Presenter;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(StartupInstaller) + " in " + nameof(Installers))]
    public class StartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadingScreenView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingScreenPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<StartupPresenter>().AsSingle();
        }
    }
}