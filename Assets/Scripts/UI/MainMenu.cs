using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.Services.Loading;

namespace Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _startGameButton;
        [SerializeField]
        private Button _loadGameButton;
        private ILoadingScreenProvider _loadingScreenProvider;

        [Inject]
        private void Construct(ILoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }
        private void Awake()
        {
            _startGameButton.onClick.AddListener(OnStartGame);
            _loadGameButton.onClick.AddListener(OnLoadGame);
        }
        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveListener(OnStartGame);
            _loadGameButton.onClick.RemoveListener(OnLoadGame);
        }
        private void OnStartGame()
        {
            Queue<LazyLoadingOperation> operations = new Queue<LazyLoadingOperation>();
            Func<ILoadingOperation> gameLoadingOperation = () => new SceneLoadingOperation(2);
            Func<ILoadingOperation> cleanupSceneOperation = () => new SceneCleanupOperation(1);
            operations.Enqueue(gameLoadingOperation);
            operations.Enqueue(cleanupSceneOperation);
            _loadingScreenProvider.LoadAndDestroyAsync(operations);
        }
        private void OnLoadGame()
        {
            
        }
    }
}