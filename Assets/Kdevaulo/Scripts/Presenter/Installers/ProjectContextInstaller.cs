using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Service;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(ProjectContextInstaller) + " in " + nameof(Installers))]
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneChangeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SessionModel>().AsSingle();
        }
    }
}