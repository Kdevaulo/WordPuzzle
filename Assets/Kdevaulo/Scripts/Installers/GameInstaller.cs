using Kdevaulo.WordPuzzle.Data;
using Kdevaulo.WordPuzzle.Views;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(GameInstaller) + " in " + nameof(Installers))]
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ClustersData _clustersData;
        [SerializeField] private WordsData _wordsData;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().AsSingle();
            Container.Bind<MainView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ClusterSpawner>().AsSingle().WithArguments(_clustersData.Clusters);

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
        }
    }
}