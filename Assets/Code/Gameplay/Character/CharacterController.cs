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
        private readonly CircleLayerCheckView _groundedCheckView;
        
        private readonly CircleLayerCheckModel _groundedCheckModel;
        private readonly MovementModel _movementModel;
        private readonly JumpModel _jumpModel;

        private float _direction;

        public CharacterController(CharacterView characterView, CircleLayerCheckView groundedCheckView, 
            UpdateManager updateManager, InputService inputService)
        {
            _characterView = characterView;
            _groundedCheckView = groundedCheckView;
            
            _groundedCheckModel = new CircleLayerCheckModel(
                _groundedCheckView.Radius,  
                _groundedCheckView.CheckLayers,
                _groundedCheckView.transform
            );

            _movementModel = new MovementModel(
                _characterView.Rigidbody,
                _characterView.Config.Speed,
                _characterView.Config.GravityModifier,
                _groundedCheckModel,
                false,
                _characterView.Config.Accel
            );

            _jumpModel = new JumpModel(
                _characterView.Rigidbody,
                _groundedCheckModel,
                _characterView.Config.JumpForce,
                _characterView.Config.MaxJumps
            );

            updateManager.Register((ITickable)this);
            updateManager.Register((IFixedTickable)this);
            SetInputActions(inputService);
        }

        public void Tick(float deltaTime)
        {
            _groundedCheckModel.Tick();
            _jumpModel.Tick();
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

            inputService.PlayerInput.Gameplay.Jump.started += InputJumpAction;
        }
        
        
        private void InputMoveAction(InputAction.CallbackContext ctx)
        {
            _direction = ctx.ReadValue<float>();
        }
        
        private void InputJumpAction(InputAction.CallbackContext ctx)
        {
            _jumpModel.Jump();
        }
    }
}