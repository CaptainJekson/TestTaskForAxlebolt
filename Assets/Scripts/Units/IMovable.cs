using UnityEngine;

namespace Units
{
    public interface IMovable
    {
        void Move(Vector3 targetPosition);
        
        public Transform GetTransform();
    }
}