using System;
using DG.Tweening;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class GameOverWindow : Window
    {
        [SerializeField] private Button _restartGameButton;

        private Action _restartCallback;
        
        public void SetRestartCallback(Action restartCallback)
        {
            _restartCallback = restartCallback;
        }
        
        protected override void Init()
        {
            _restartGameButton.onClick.AddListener(OnRestartGameButtonClick);
        }

        public override void Open()
        {
            base.Open();
            transform.DOScale(Vector3.one, 0.3f).From(Vector3.zero).SetEase(Ease.OutBack);
        }

        public override void Close()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));
            sequence.AppendCallback(base.Close);
        }

        private void OnRestartGameButtonClick()
        {
            _restartCallback?.Invoke();
            Close();
        }
    }
}