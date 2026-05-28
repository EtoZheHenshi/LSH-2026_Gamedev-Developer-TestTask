using Code.Core.Events;
using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Character;
using Code.Gameplay.General;
using Code.Gameplay.Systems;
using Code.Infrastructure.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using CharacterController = Code.Gameplay.Character.CharacterController;

namespace Code.Core
{
    public sealed class Bootstrap : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; }
        public static string NextSceneName;
        
        [Header("Next Scene")]
        [SerializeField] private string _nextSceneName;
        
        [Header("Character")]
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private BoxLayerCheckView _characterGroundedCheckView;
        
        [Header("Don't Destroy Objects")]
        [SerializeField] private GameObject[] _dontDestroyObjects;
        
        private EventBus _eventBus;
        private TimerSystem _timerSystem;
        private InputService _inputService;
        
        private void Awake()
        {
            RegisterServices();

            for (int i = 0; i < _dontDestroyObjects.Length; i++)
            {
                DontDestroyOnLoad(_dontDestroyObjects[i]);
            }

            CharacterController character = new CharacterController(
                _characterView,
                _characterGroundedCheckView, 
                ServiceLocator.Get<UpdateManager>(),
                ServiceLocator.Get<InputService>()
                );
            
            _eventBus.Subscribe<LevelStartEvent>(e =>
            {
                character.UnstopCharacter();
                _inputService.CurrentMap.Enable();
                _timerSystem.StartTimer();
            });
            
            _eventBus.Subscribe<LevelFinishedEvent>(e =>
            {
                character.StopCharacter();
                _inputService.CurrentMap.Disable();
                _timerSystem.StopTimer();
            });
            
            ServiceLocator.Get<UpdateManager>().Register(_timerSystem);
            
            IsInitialized = true;
            NextSceneName ??= _nextSceneName;
            SceneManager.LoadScene(NextSceneName);
        }

        private void RegisterServices()
        {
            ServiceLocator.Register(new UpdateManager());
            
            _inputService = new InputService();
            ServiceLocator.Register(_inputService);
            
            _eventBus = new EventBus();
            ServiceLocator.Register(_eventBus);
            
            ServiceLocator.Register(new CoinSystem(_eventBus));
            
            ServiceLocator.Register(new ScoreSystem(_eventBus));
            
            _timerSystem = new TimerSystem();
            ServiceLocator.Register(_timerSystem);
        }
    }
}