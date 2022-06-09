using System;
using Units.PlayerUnits;
using UnityEngine;

namespace Units.EnemyUnits
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _retreatSpeed;

        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private Transform _playerUnitPosition;
        private bool _isMoveRetreat;
        
        public event Action Destroyed;
        
        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var playerUnit = other.gameObject.GetComponent<IPlayer>();

            if (playerUnit == null)
                return;
            
            _playerUnitPosition = playerUnit.GetTransform();
            _isMoveRetreat = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<IPlayer>() == null) 
                return;
            
            _isMoveRetreat = false;
        }

        public void Move(Vector3 targetPosition)
        {
            if (_isMoveRetreat)
            {
                MoveToRetreat();
            }
            else
            {
                MoveToTarget(targetPosition);
            }
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        public void Destroy()
        {
            Destroyed?.Invoke();
            Destroy(gameObject);
        }

        private void MoveToTarget(Vector3 targetPosition)
        {
            transform.position = Vector3.MoveTowards(_transform.position, targetPosition,
                Time.deltaTime * _speed);

            if (Vector3.Distance(targetPosition, _transform.position) <= 0)
            {
                Destroy();
            }
        }

        private void MoveToRetreat()
        {
            var currentPosition = _transform.position;
            var playerPosition = _playerUnitPosition.position;
            var difference = (currentPosition - playerPosition).magnitude;
            
            transform.position = Vector3.MoveTowards(currentPosition,
                difference > 0 ? currentPosition - playerPosition : currentPosition + playerPosition,
                Time.deltaTime * _retreatSpeed);
        }
    }
}