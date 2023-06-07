using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.Services.Loading;

namespace Core.UI
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Button _backToMainMenuButton;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private Button _pauseButton;

        private ILoadingScreenProvider _loadingScreenProvider;

        private readonly int ShownHash = Animator.StringToHash("Shown");

        [Inject]
        private void Construct(ILoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }

        private void Awake()
        {
            _pauseButton.onClick.AddListener(OnPauseClick);
            _backToMainMenuButton.onClick.AddListener(OnBackToMainMenu);
            _closeButton.onClick.AddListener(OnClose);
        }
        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseClick);
            _backToMainMenuButton.onClick.RemoveListener(OnBackToMainMenu);
            _closeButton.onClick.RemoveListener(OnClose);
        }
        private void OnPauseClick()
        {
            bool isShown = _animator.GetBool(ShownHash);
            _animator.SetBool(ShownHash, !isShown);
        }
        private void OnBackToMainMenu()
        {
            Queue<LazyLoadingOperation> operations = new Queue<LazyLoadingOperation>();
            Func<ILoadingOperation> menuLoadingOperation = () => new SceneLoadingOperation(1);
            Func<ILoadingOperation> cleanupSceneOperation = () => new SceneCleanupOperation(2);
            operations.Enqueue(menuLoadingOperation);
            operations.Enqueue(cleanupSceneOperation);
            _loadingScreenProvider.LoadAndDestroyAsync(operations);
        }
        private void OnClose()
        {
            _animator.SetBool(ShownHash, false);
        }
    }
}