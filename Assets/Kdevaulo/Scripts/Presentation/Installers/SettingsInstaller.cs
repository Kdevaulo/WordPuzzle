using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Presentation.Installers
{
    [AddComponentMenu(nameof(SettingsInstaller) + " in " + nameof(Installers))]
    public class SettingsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        }
    }
}