using System;
using Code.Core.Update;
using Code.Gameplay.General;
using Code.Infrastructure.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Character
{
    public sealed class CharacterController : ITickable, IFixedTickable
    {
        private readonly CharacterView _characterView;
        private GroundedCheckView _groundedCheckView;
        private GroundedCheckModel _groundedCheckModel;
        private MovementModel _movementModel;

        private float _direction;

        public CharacterController(CharacterView characterView, GroundedCheckView groundedCheckView, 
            UpdateManager updateManager, InputService inputService)
        {
            _characterView = characterView;
            _groundedCheckView = groundedCheckView;
            
            _groundedCheckModel = new GroundedCheckModel(
                _groundedCheckView.Radius,  
                _groundedCheckView.GroundLayers,
                _groundedCheckView.transform
                );

            _movementModel = new MovementModel(
                _characterView.Rigidbody,
                _characterView.Config.Speed,
                _characterView.Config.GravityModifier,
                _groundedCheckModel,
                true,
                _characterView.Config.Accel
            );

            updateManager.Register((ITickable)this);
            updateManager.Register((IFixedTickable)this);
            SetInputActions(inputService);
        }

        public void Tick(float deltaTime)
        {
            _groundedCheckModel.Tick();
        }

        public void FixedTick(float fixedDeltaTime)
        {
            _movementModel.Tick(fixedDeltaTime);
            _movementModel.MoveHorizontal(fixedDeltaTime, _direction);
        }

        private void SetInputActions(InputService inputService)
        {
            inputService.PlayerInput.Gameplay.Move.performed += InputMoveAction;
            inputService.PlayerInput.Gameplay.Move.canceled += InputMoveAction;
        }
        
        
        private void InputMoveAction(InputAction.CallbackContext ctx)
        {
            _direction = ctx.ReadValue<float>();
            Debug.Log(ctx.ReadValue<float>());
        }
    }
}