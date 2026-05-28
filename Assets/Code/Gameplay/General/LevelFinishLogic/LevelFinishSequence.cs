using Code.Core.Events;
using Code.Core.Update;
using Code.Gameplay.Systems;
using Code.Infrastructure.Input;
using Code.UI;

namespace Code.Gameplay.General.LevelFinishLogic
{
    public sealed class LevelFinishSequence : ITickable
    {
        private readonly InputService _input;
        private readonly CharacterFinishLevelMovement _finishAnimation;
        private readonly EventBus _eventBus;
        private readonly TimerSystem _timerSystem;
        private readonly ScoreSystem _scoreSystem;
        private readonly UIController _uiController;
        
        private bool _active;
        private float _timer;
        private int _phase;

        public LevelFinishSequence(InputService input, CharacterFinishLevelMovement finishAnimation, EventBus eventBus,
            TimerSystem timerSystem, ScoreSystem scoreSystem, UIController uiController)
        {
            _input = input;
            _finishAnimation = finishAnimation;
            _eventBus = eventBus;
            _timerSystem = timerSystem;
            _scoreSystem = scoreSystem;
            _uiController = uiController;
        }
        
        public void Tick(float deltaTime)
        {
            if (!_active) return;
            
            _timer += deltaTime;
            
            SelectPhase(deltaTime);
        }

        public void StartSequence()
        {
            _input.CurrentMap.Disable();
            
            _eventBus.Publish(new LevelFinishedEvent());
            
            _finishAnimation.GetReady();
            _active = true;
            _timer = 0;
            _phase = 0;
        }

        private void SelectPhase(float deltaTime)
        {
            switch (_phase)
            {
                case 0:
                    HandleFinishAnimation(deltaTime);
                    break;
                case 1:
                    HandleScoreCountdown();
                    break;
                case 2:
                    HandleRestartLevel();
                    break;
            }
        }
        
        private void HandleFinishAnimation(float deltaTime)
        {
            _finishAnimation.Tick(deltaTime);

            if (_finishAnimation.Complete)
            {
                _phase++;
            }
        }

        private void HandleScoreCountdown()
        {
            if (_timer < 0.02f) return;

            _timer = 0f;

            if (_timerSystem.TimeLeft > 0f)
            {
                _timerSystem.Remove(1);
                
                _scoreSystem.Add(50);
            }
            else
            {
                _phase++;
            }
        }
        
        private void HandleFinishScreen()
        {
            _uiController.Activate();
            
            _active = false;
        }

        private void HandleRestartLevel()
        {
            RestartLevel.Restart(true);
        }
    }
}