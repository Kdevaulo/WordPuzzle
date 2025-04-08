using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(VictoryInstaller) + " in " + nameof(Installers))]
    public class VictoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
        }
    }
}