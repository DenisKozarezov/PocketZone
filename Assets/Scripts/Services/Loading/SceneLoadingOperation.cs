using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Core.Services.Loading;

namespace Core.Services.Loading
{
    public class SceneLoadingOperation : ILoadingOperation
    {
        private readonly int _buildIndex;
        public string Description => "Loading scene...";
        public float Progress { get; private set; }
        public bool IsCompleted => Progress == 1f;

        public SceneLoadingOperation(int buildIndex)
        {
            _buildIndex = buildIndex;
        }

        public IEnumerator AwaitForLoad()
        {
            var operation = SceneManager.LoadSceneAsync(_buildIndex, LoadSceneMode.Additive);

#if UNITY_EDITOR
            if (operation == null)
            {
                throw new InvalidOperationException("There is no scene with such index: " + _buildIndex);
            }
#endif

            while (!operation.isDone)
            {
                Progress = operation.progress;
                yield return null;
            }
            Progress = 1f;
        }
    }
}