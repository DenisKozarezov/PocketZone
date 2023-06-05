using UnityEngine;
using Zenject;
using Core.Models;
using Core.Models.Units;

namespace Core.Infrastructure.Installers
{
    [CreateAssetMenu(menuName = "Configuration/Settings/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [Header("Game Configuration")]
        [SerializeField]
        private PlayerConfig _playerSettings;
        [SerializeField]
        private EnemyConfig _enemyConfig;

        public override void InstallBindings()
        {
            BindAllSettings();
        }

        private void BindAllSettings()
        {
            Container.BindInstance(_playerSettings).AsSingle().IfNotBound();
            Container.BindInstance(_enemyConfig).AsSingle().IfNotBound();
        }
    }
}