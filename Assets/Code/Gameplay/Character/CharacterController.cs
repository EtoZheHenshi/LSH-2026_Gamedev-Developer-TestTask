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
        private readonly MovementModel _movementModel;
        private readonly JumpModel _jumpModel;
        private readonly CharacterStompModel _stompModel;

        private float _direction;

        public CharacterController(CharacterView characterView, CircleLayerCheckView groundedCheckView,
            UpdateManager updateManager, InputService inputService)
        {
            //Initialization
            CircleLayerCheckModel groundedCheckModel = new CircleLayerCheckModel(
                groundedCheckView.Radius,  
                groundedCheckView.CheckLayers,
                groundedCheckView.transform
            );

            _movementModel = new MovementModel(
                characterView.Rigidbody,
                characterView.Config.Speed,
                characterView.Config.GravityModifier,
                groundedCheckModel,
                false,
                characterView.Config.Accel
            );

            _jumpModel = new JumpModel(
                characterView.Rigidbody,
                groundedCheckModel,
                characterView.Config.JumpForce,
                characterView.Config.MaxJumps
            );

            _stompModel = new CharacterStompModel(characterView.Rigidbody, characterView.Config.StompJumpForce);

            //Subscribing
            characterView.OnStomp += _stompModel.Stomp;
            
            updateManager.Register((ITickable)this);
            updateManager.Register((IFixedTickable)this);
            SetInputActions(inputService);
        }

        public void Tick(float deltaTime)
        {
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