using UnityEngine.EventSystems;
using Core.Services.Loading;
using Zenject;

namespace Core.Infrastructure.Installers
{
    internal class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILoadingScreenProvider>().To<LoadingScreenProvider>().AsSingle().NonLazy();
            Container.Bind<ICoroutineRunner>().To<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}