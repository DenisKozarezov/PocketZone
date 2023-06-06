using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.Loading
{
    internal sealed class LoadingScreenProvider : ILoadingScreenProvider
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public LoadingScreenProvider(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        private LoadingScreen LoadScreen()
        {
            var load = Resources.Load<LoadingScreen>("Loading Screen");
            var obj = GameObject.Instantiate(load);
            GameObject.DontDestroyOnLoad(obj);
            return obj;
        }
        private IEnumerator LoadOperations(Queue<LazyLoadingOperation> operations)
        {
            LoadingScreen loadingScreen = LoadScreen();

            yield return loadingScreen.LoadAsync(operations);

            GameObject.Destroy(loadingScreen.gameObject);
        }
        public void LoadAndDestroyAsync(Queue<LazyLoadingOperation> operations)
        {
            _coroutineRunner.StartCoroutine(LoadOperations(operations));
        }
    }
}
