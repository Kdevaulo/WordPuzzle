using Kdevaulo.WordPuzzle.Core;
using Kdevaulo.WordPuzzle.Model;
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
            Container.Bind<LevelLoaderService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<WordModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<WordPresenter>().AsSingle();

            Container.Bind<DragHandler>().AsSingle().NonLazy();
            Container.Bind<WordsData>().FromInstance(_wordsData).AsSingle();

            Container.Bind<MainView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<ClusterSpawner>().AsSingle()
                .WithArguments(_clustersData.Clusters, Container);

            Container.BindInterfacesAndSelfTo<LocalLevelLoader>().AsSingle();

            Container.BindInterfacesAndSelfTo<WordView>().FromComponentsInHierarchy().AsTransient();
        }
    }
}