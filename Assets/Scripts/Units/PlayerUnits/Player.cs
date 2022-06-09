using UnityEngine;

namespace Units.PlayerUnits
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _scaleStep;
        [SerializeField] private float _limitIncreaseSize;
        [SerializeField] private float _limitDecreaseSize;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
        
        public Transform GetTransform()
        {
            return _transform;
        }

        public void Move(Vector3 targetPosition)
        {
            _transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, Time.deltaTime * _speed);
        }

        public void IncreaseSize()
        {
            if(_transform.localScale.x >= _limitIncreaseSize)
                return;
            
            _transform.localScale +=  new Vector3(_scaleStep, _scaleStep, _scaleStep);
        }

        public void DecreaseSize()
        {
            if(_transform.localScale.x <= _limitDecreaseSize)
                return;
            
            _transform.localScale -=  new Vector3(_scaleStep, _scaleStep, _scaleStep);
        }
    }
}
