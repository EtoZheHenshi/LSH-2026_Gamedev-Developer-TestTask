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
            
            IsInitialized = true;
            NextSceneName ??= _nextSceneName;
            SceneManager.LoadScene(NextSceneName);
        }

        private void RegisterServices()
        {
            ServiceLocator.Register(new UpdateManager());
            
            ServiceLocator.Register(new InputService());
            
            EventBus eventBus = new EventBus();
            ServiceLocator.Register(eventBus);
            
            ServiceLocator.Register(new CoinSystem(eventBus));
            
            ServiceLocator.Register(new ScoreSystem(eventBus));
        }
    }
}