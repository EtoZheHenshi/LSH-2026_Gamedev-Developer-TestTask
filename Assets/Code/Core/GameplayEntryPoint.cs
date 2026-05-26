using System;
using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Enemies.Types.Goomba;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core
{
    public sealed class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GoombaView _goombaView;
        [SerializeField] private Transform _playerStartPosition;

        private GameObject _player;

        private void Start()
        {
            if (Bootstrap.IsInitialized == false)
            {
                Bootstrap.NextSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene("Bootstrap");
                return;
            }
            
            _player = GameObject.FindWithTag("Player");
            _player.transform.position = _playerStartPosition.position;
            GoombaController goomba = new GoombaController(_goombaView, ServiceLocator.Get<UpdateManager>());
        }
    }
}