using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Enemies.Behaviours;
using Code.Gameplay.General;
using Code.Gameplay.Systems;

namespace Code.Gameplay.Enemies.Types.Goomba
{
    public sealed class GoombaController : ITickable, IFixedTickable
    {
        private readonly PatrolMovementModel _movementModel;
        private readonly DeathModel _deathModel;

        public GoombaController(GoombaView view, UpdateManager updateManager)
        {
            //Initialization
            GoombaConfig config = view.Config;
            
            _movementModel = new PatrolMovementModel(
                view.Rigidbody,
                config.Speed,
                config.GravityModifier,
                new BoxLayerCheckModel(view.GroundLayerCheck)
            );
            
            _deathModel = new DeathModel();

            //Subscribing
            Sub();
            
            _deathModel.OnDie += view.Destroy;
            view.OnDestroyEvent += UnSub;
            
            return;

            void Sub()
            {
                view.OnCollisionEnterEvent += _movementModel.SwitchDirection;
                view.OnStomp += AddScore;
                view.OnStomp += _deathModel.Die;
            
                updateManager.Register((ITickable)this);
                updateManager.Register((IFixedTickable)this);
            }

            void UnSub()
            {
                updateManager.Remove((ITickable)this);
                updateManager.Remove((IFixedTickable)this);
                
                
                view.OnStomp -= AddScore;
                view.OnStomp -= _deathModel.Die;
                view.OnCollisionEnterEvent -= _movementModel.SwitchDirection;
            }
        }

        public void Tick(float deltaTime)
        {
            _movementModel.Tick(deltaTime);
        }

        public void FixedTick(float fixedDeltaTime)
        {
            _movementModel.FixedTick(fixedDeltaTime);
        }

        private void AddScore()
        {
            ServiceLocator.Get<ScoreSystem>().Add(200);
        }
    }
}