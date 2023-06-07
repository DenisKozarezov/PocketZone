using UnityEngine;
using Zenject;
using Cinemachine;
using Core.Factories;
using Core.Services;
using Core.Services.Input;
using Core.UI;
using Core.Weapons;
using Core.Services.Inventory;

namespace Core.Infrastructure.Installers
{
    internal class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private GameObject _labelVFXPrefab;

        public override void InstallBindings()
        {            
            BindFactories();
            BindInput();
            BindServices();

            Container.BindInterfacesTo<Level>().AsSingle().NonLazy();
        }
        private void BindInput()
        {
            Container.Bind<IInputService>().To<MobilePlatformInput>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
        private void BindServices()
        {
            Container.Bind<ICinemachineCamera>().To<CinemachineVirtualCamera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<HealthBarManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<InventoryManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<TimeUpdateService>().AsSingle().NonLazy();
        }
        private void BindFactories()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();

            Container.BindFactory<Vector2, Vector2, string, float, DamageVFX, DamageVFXFactory>().FromMonoPoolableMemoryPool(x => x
               .WithInitialSize(10)
               .FromComponentInNewPrefab(_labelVFXPrefab)
               .UnderTransformGroup("UI VFX"));

            Container.BindFactoryCustomInterface<Vector2, Quaternion, float, float, Bullet, BulletFactory, IBulletFactory>()
               .FromMonoPoolableMemoryPool(x => x
               .WithInitialSize(10)
               .FromComponentInNewPrefab(_bulletPrefab)
               .UnderTransformGroup("Bullets"));
        }
    }
}