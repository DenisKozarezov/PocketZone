using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Core.Services.Loading;

namespace Core.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]
        private EventSystem _eventSystem;
        private ILoadingScreenProvider _loadingScreenProvider;

        [Inject]
        private void Construct(ILoadingScreenProvider provider)
        {
            _loadingScreenProvider = provider;
        }

        private void Awake()
        {
            DontDestroyOnLoad(_eventSystem.gameObject);
        }
        private void Start()
        {
            LoadProcess();
        }
        private void LoadProcess()
        {
            Queue<LazyLoadingOperation> operations = new Queue<LazyLoadingOperation>();
            Func<ILoadingOperation> menuLoadingOperation = () => new SceneLoadingOperation(1);
            Func<ILoadingOperation> bootstrapCleanupOperation = () => new SceneCleanupOperation(0);
            operations.Enqueue(menuLoadingOperation);
            operations.Enqueue(bootstrapCleanupOperation);
            _loadingScreenProvider.LoadAndDestroyAsync(operations);
        }
    }
}