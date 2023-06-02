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
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        }
    }
}