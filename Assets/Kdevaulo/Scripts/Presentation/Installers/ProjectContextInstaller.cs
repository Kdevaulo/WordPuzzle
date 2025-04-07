using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presentation.Installers
{
    [AddComponentMenu(nameof(ProjectContextInstaller) + " in " + nameof(Installers))]
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        }
    }
}