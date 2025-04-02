using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(GameInstaller) + " in " + nameof(Installers))]
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        }
    }
}