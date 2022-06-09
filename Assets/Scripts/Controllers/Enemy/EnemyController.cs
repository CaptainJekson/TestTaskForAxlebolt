using UI.Base;
using UI.Windows;
using Units.EnemyUnits;
using UnityEngine;
using Utility;

namespace Controllers.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IEnemy))] private GameObject _enemyTemplate;
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPosition;

        private IEnemy _enemy;
        private GameOverWindow _gameOverWindow;

        private void Awake()
        {
            CreateEnemy();
            _gameOverWindow = _uiRoot.GetWindow<GameOverWindow>();
            _gameOverWindow.SetRestartCallback(CreateEnemy);
        }

        private void Update()
        {
            _enemy?.Move(_endPosition.position);
        }
        
        private void CreateEnemy()
        {
            _enemy = Instantiate(_enemyTemplate, _startPoint.position, Quaternion.identity).GetComponent<IEnemy>();
            _enemy.Destroyed += OnEnemyDestroyed;
        }

        private void OnEnemyDestroyed()
        {
            _enemy.Destroyed -= OnEnemyDestroyed;
            _enemy = null;
            _gameOverWindow.Open();
        }
    }
}