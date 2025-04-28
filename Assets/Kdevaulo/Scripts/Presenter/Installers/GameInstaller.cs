using Kdevaulo.WordPuzzle.Model;
using Kdevaulo.WordPuzzle.Presenter;
using Kdevaulo.WordPuzzle.Service;

using UnityEngine;

using Zenject;

namespace Kdevaulo.WordPuzzle.Installers
{
    [AddComponentMenu(nameof(GameInstaller) + " in " + nameof(Installers))]
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ClustersConfig _clustersConfig;
        [SerializeField] private WordsConfig _wordsConfig;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoadingSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ClusterSpawner>().AsSingle()
                .WithArguments(_clustersConfig.Clusters, Container);

            Container.BindInterfacesAndSelfTo<LocalLevelLoader>().AsSingle();

            Container.Bind<WordsConfig>().FromInstance(_wordsConfig).AsSingle();

            Container.Bind<DragHandler>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<ValidationSystem>().AsSingle();

            Container.BindInterfacesAndSelfTo<ValidationView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<WordView>().FromComponentsInHierarchy().AsTransient();
            Container.BindInterfacesAndSelfTo<MainGameView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<ValidationPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClustersPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<WordPresenter>().AsSingle();

            Container.BindInterfacesAndSelfTo<WordModel>().AsSingle();
        }
    }
}