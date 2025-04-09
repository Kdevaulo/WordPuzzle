using Kdevaulo.WordPuzzle.Service;
using Kdevaulo.WordPuzzle.View;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(ProjectContextInstaller) + " in " + nameof(Installers))]
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneManagerAdapter>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
        }
    }
}