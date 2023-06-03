using Zenject;
using Cinemachine;
using Core.Factories;
using Core.Services.Input;
using Core.UI;

namespace Core.Infrastructure.Installers
{
    internal class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Level>().AsSingle().NonLazy();
            Container.Bind<ICinemachineCamera>().To<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<HealthBarManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeUpdateService>().AsSingle().NonLazy();

            BindFactories();
            BindInput();
        }
        private void BindInput()
        {
            Container.Bind<JoystickHandler>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IInputService>().To<JoystickInput>().AsSingle().NonLazy();
        }
        private void BindFactories()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
        }
    }
}