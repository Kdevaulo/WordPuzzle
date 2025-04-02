using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(MainMenuInstaller) + " in " + nameof(Installers))]
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        }
    }
}