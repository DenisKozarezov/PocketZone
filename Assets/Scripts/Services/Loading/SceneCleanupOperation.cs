using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services.Loading
{
    internal sealed class SceneCleanupOperation : ILoadingOperation
    {
        private readonly int _buildIndex;
        public string Description => "Cleaning scene...";
        public float Progress { get; private set; }
        public bool IsCompleted => Progress == 1f;

        public SceneCleanupOperation(int buildIndex)
        {
            _buildIndex = buildIndex;
        }

        public IEnumerator AwaitForLoad()
        {
            var operation = SceneManager.UnloadSceneAsync(_buildIndex);

            if (operation == null)
            {
                Debug.LogWarning($"Unable to clean the scene with such index <b><color=yellow>{_buildIndex}</color></b>. Skipping the operation...");
                Progress = 1f;
                yield break;
            }

            while (!operation.isDone)
            {
                Progress = operation.progress;
                yield return null;
            }
            Progress = 1f;
        }
    }
}
