using Code.Core.Update;
using Code.Gameplay.Enemies.Behaviours;
using Code.Gameplay.General;

namespace Code.Gameplay.Enemies.Types.Goomba
{
    public sealed class GoombaController : ITickable, IFixedTickable
    {
        private PatrolMovementModel _movementModel;

        public GoombaController(GoombaView view, UpdateManager updateManager)
        {
            GoombaConfig config = view.Config;
            
            _movementModel = new PatrolMovementModel(
                view.Rigidbody,
                config.Speed,
                config.GravityModifier,
                new CircleLayerCheckModel(view.GroundLayerCheck)
            );

            view.OnCollisionEnterEvent += _movementModel.SwitchDirection;
            
            updateManager.Register((ITickable)this);
            updateManager.Register((IFixedTickable)this);
        }

        public void Tick(float deltaTime)
        {
            _movementModel.Tick(deltaTime);
        }

        public void FixedTick(float fixedDeltaTime)
        {
            _movementModel.FixedTick(fixedDeltaTime);
        }
    }
}