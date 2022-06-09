using System;

namespace Units
{
    public interface IDestroyer
    {
        event Action Destroyed;
        void Destroy();
    }
}