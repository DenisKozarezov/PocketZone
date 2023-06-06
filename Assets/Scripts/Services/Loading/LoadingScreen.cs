using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Core.Services.Loading
{
    internal sealed class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _progressBar;
        [SerializeField]
        private TextMeshProUGUI _description;
        [SerializeField, Range(0f, 1f)]
        private float _progressBarSpeed;

        private Tweener _tweener;

        public IEnumerator LoadAsync(Queue<LazyLoadingOperation> operations)
        {
            foreach (var lazyOperation in operations)
            {
                ILoadingOperation loadingOperation = lazyOperation.Value;

                ResetFill();
                _description.text = loadingOperation.Description;
                StartCoroutine(loadingOperation.AwaitForLoad());
                while (!loadingOperation.IsCompleted)
                {
                    SetFillAmount(loadingOperation.Progress);
                    yield return null;
                }
                SetFillAmount(1f);
                yield return new WaitForSeconds(_progressBarSpeed);
            }
        }
        private void SetFillAmount(float value)
        {
            _tweener?.Kill();
            _tweener = _progressBar.
                DOFillAmount(value, _progressBarSpeed).
                SetLink(gameObject).
                SetEase(Ease.Linear);
        }
        private void ResetFill()
        {
            _tweener?.Kill();
            _progressBar.fillAmount = 0f;
        }
    }
}
