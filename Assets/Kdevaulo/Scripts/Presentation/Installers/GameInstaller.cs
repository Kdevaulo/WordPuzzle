using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Data;
using Kdevaulo.WordPuzzle.Presentation.Views;

using UnityEngine;

using Zenject;

using ClustersData = Kdevaulo.WordPuzzle.Presentation.Data.ClustersData;

namespace Kdevaulo.WordPuzzle.Presentation.Installers
{
    [AddComponentMenu(nameof(GameInstaller) + " in " + nameof(Installers))]
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ClustersData _clustersData;
        [SerializeField] private WordsData _wordsData;

        public override void InstallBindings()
        {
            // core
            Container.Bind<AbstractLevelLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            // unity
            Container.Bind<DragHandler>().AsSingle().NonLazy();

            Container.Bind<MainView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<ClusterSpawner>().AsSingle()
                .WithArguments(_clustersData.Clusters, Container);

            Container.BindInterfacesAndSelfTo<LevelLoader>().AsSingle();

            Container.Bind<WordView>().FromComponentsInHierarchy().AsTransient();
        }
    }
}