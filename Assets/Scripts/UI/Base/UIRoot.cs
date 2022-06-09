using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Base
{
    public sealed class UIRoot : MonoBehaviour
    {
        [SerializeField] private List<Window> _windows;

        private void Awake()
        {
            DisableAllWindow();
        }

        public T GetWindow<T>() where T : Window
        {
            return (from window in _windows where window.GetType() == typeof(T) select window as T).FirstOrDefault();
        }

        private void DisableAllWindow()
        {
            foreach (var window in _windows)
            {
                window.gameObject.SetActive(false);
            }
        }
    }
}