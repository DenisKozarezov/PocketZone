using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.Services.Loading;
using Core.Services.Serialization;

namespace Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _newGameButton;
        [SerializeField]
        private Button _loadGameButton;
        private ILoadingScreenProvider _loadingScreenProvider;
        private GameState _gameState;

        [Inject]
        private void Construct(ILoadingScreenProvider loadingScreenProvider, GameState gameState)
        {
            _loadingScreenProvider = loadingScreenProvider;
            _gameState = gameState;
        }
        private void Awake()
        {
            _newGameButton.onClick.AddListener(LoadGame);
            _loadGameButton.onClick.AddListener(OnLoadingSave);
            _loadGameButton.interactable = _gameState.HasAnySave();
        }
        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveListener(LoadGame);
            _loadGameButton.onClick.RemoveListener(OnLoadingSave);
        }
        private void LoadGame()
        {
            Queue<LazyLoadingOperation> operations = new Queue<LazyLoadingOperation>();
            Func<ILoadingOperation> gameLoadingOperation = () => new SceneLoadingOperation(2);
            Func<ILoadingOperation> cleanupSceneOperation = () => new SceneCleanupOperation(1);
            operations.Enqueue(gameLoadingOperation);
            operations.Enqueue(cleanupSceneOperation);
            _loadingScreenProvider.LoadAndDestroyAsync(operations);
        }
        private void OnLoadingSave()
        {
            GameState.IsLoadingGame = true;
            LoadGame();
        }
    }
}