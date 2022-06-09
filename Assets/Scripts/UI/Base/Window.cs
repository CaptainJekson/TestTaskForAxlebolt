using UnityEngine;

namespace UI.Base
{
    public abstract class Window : MonoBehaviour
    {
        private bool _isOpen;
        private bool _isClose;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            if (!_isOpen)
            {
                _isOpen = true;
                return;
            }
            OnOpen();
        }

        private void OnDisable()
        {
            if (!_isClose)
            {
                _isClose = true;
                return;
            }

            OnClose();
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        protected virtual void Init()
        {
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }
    }
}