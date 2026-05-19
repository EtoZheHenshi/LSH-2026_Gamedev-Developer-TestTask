using Code.Core.Services;
using Code.Core.Update;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] public string nextSceneName;
        private void Awake()
        {
            RegisterServices();
            
            SceneManager.LoadScene(nextSceneName);
        }

        private void RegisterServices()
        {
            ServiceLocator.Register(new UpdateManager());
        }
    }
}