using Kdevaulo.WordPuzzle.Presenter;
using Kdevaulo.WordPuzzle.View;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(MainMenuInstaller) + " in " + nameof(Installers))]
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
        }
    }
}