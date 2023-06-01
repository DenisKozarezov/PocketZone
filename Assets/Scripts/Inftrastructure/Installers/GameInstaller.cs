using Zenject;
using Core.Factories;

namespace Core.Infrastructure.Installers
{
    internal class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerFactory>().To<UnityPlayerFactory>().AsSingle();
        }
    }
}