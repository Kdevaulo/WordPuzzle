using Kdevaulo.WordPuzzle.Presenter;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(VictoryInstaller) + " in " + nameof(Installers))]
    public class VictoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<VictoryView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<VictoryPresenter>().AsSingle();
        }
    }
}