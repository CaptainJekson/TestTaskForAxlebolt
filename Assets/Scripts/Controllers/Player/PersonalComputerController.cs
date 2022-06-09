using Units.PlayerUnits;
using UnityEngine;
using Utility;

namespace Controllers.Player
{
    public class PersonalComputerController : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IPlayer))] private GameObject _playerUnit;

        private Camera _mainCamera;
        private IPlayer _player;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _player = _playerUnit.GetComponent<IPlayer>();
        }

        private void Update()
        {
            var targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _player.Move(targetPosition);

            if (Input.mouseScrollDelta.y > 0)
            {
                _player.IncreaseSize();
            }
            
            if(Input.mouseScrollDelta.y < 0)
            {
                _player.DecreaseSize();
            }
        }
    }
}