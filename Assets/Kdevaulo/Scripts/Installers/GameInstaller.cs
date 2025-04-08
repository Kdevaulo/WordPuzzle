using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Presenter;
using Kdevaulo.WordPuzzle.View;
using Kdevaulo.WordPuzzle.View.Data;

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
            // core
            Container.Bind<LevelLoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            // unity
            Container.Bind<DragHandler>().AsSingle().NonLazy();

            Container.Bind<MainView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<ClusterSpawner>().AsSingle()
                .WithArguments(_clustersData.Clusters, Container);

            Container.BindInterfacesAndSelfTo<LocalLevelLoader>().AsSingle();

            Container.Bind<WordView>().FromComponentsInHierarchy().AsTransient();
        }
    }
}