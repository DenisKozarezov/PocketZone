using Zenject;
using Core.Services.Loading;
using Core.Services.Serialization;

namespace Core.Infrastructure.Installers
{
    internal class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameState>().AsSingle().NonLazy();
            Container.Bind<ILoadingScreenProvider>().To<LoadingScreenProvider>().AsSingle().NonLazy();
            Container.Bind<ILocalStateSerializer>().To<LocalJSONSerializer>().AsSingle().NonLazy();
            Container.Bind<ICoroutineRunner>().To<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}