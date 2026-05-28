using System;
using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Enemies.Types.Goomba;
using Code.Gameplay.General;
using Code.Gameplay.Items.Coin;
using Code.Gameplay.Systems;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core
{
    public sealed class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GoombaView _goombaView;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private Collider2D _characterStopCollider;
        [SerializeField] private CoinView[] _coinsOnLevel;
        
        private CameraLogicModel _cameraLogicModel;

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
            
            _cameraLogicModel = new CameraLogicModel(_player.transform, _characterStopCollider, Camera.main);
            
            ServiceLocator.Get<UpdateManager>().Register(_cameraLogicModel);
            
            ServiceLocator.Get<CoinSystem>().InitializeCoins(_coinsOnLevel);
            
            GoombaController goomba = new GoombaController(_goombaView, ServiceLocator.Get<UpdateManager>());
        }
    }
}