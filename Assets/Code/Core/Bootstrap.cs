using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Character;
using Code.Gameplay.General;
using Code.Infrastructure.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using CharacterController = Code.Gameplay.Character.CharacterController;

namespace Code.Core
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Next Scene")]
        [SerializeField] public string nextSceneName;
        
        [Header("Character")]
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private GroundedCheckView _characterGroundedCheckView;
        
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
            
            SceneManager.LoadScene(nextSceneName);
        }

        private void RegisterServices()
        {
            ServiceLocator.Register(new UpdateManager());
            ServiceLocator.Register(new InputService());
        }
    }
}