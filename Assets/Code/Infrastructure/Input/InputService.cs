using Code.Core.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Infrastructure.Input
{
    public sealed class InputService : IService
    {
        private readonly PlayerInput _playerInput = new();
        
        public InputActionMap CurrentMap { get; private set; }
        public PlayerInput PlayerInput => _playerInput;

        public InputService(string startMapName = "Gameplay", bool startCursorEnable = false)
        {
            _playerInput.Enable();

            foreach (InputActionMap actionMap in _playerInput.asset.actionMaps)
            {
                actionMap.Disable();
            }
            
            SetMap(startMapName, startCursorEnable);
        }

        public void SetMap(string mapName, bool cursorEnable = false)
        {
            InputActionMap map = _playerInput.asset.FindActionMap(mapName);

            if (map == null)
            {
                Debug.LogError($"{mapName} not found");
                return;
            }
            
            CurrentMap?.Disable();
            CurrentMap = map;
            CurrentMap.Enable();

            if (cursorEnable)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}