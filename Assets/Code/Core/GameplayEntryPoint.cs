using Code.Core.Events;
using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Enemies.Types.Goomba;
using Code.Gameplay.General;
using Code.Gameplay.General.LevelFinishLogic;
using Code.Gameplay.Items.Coin;
using Code.Gameplay.Systems;
using Code.Infrastructure.Input;
using Code.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core
{
    public sealed class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GoombaView[] _goombaView;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private Collider2D _characterStopCollider;
        [SerializeField] private CoinView[] _coinsOnLevel;
        [SerializeField] private FinishFlagView _finishFlagView;
        [SerializeField] private HUDView _hudView;
        [SerializeField] private WinWindowView _winWindowView;
        
        private CameraLogicModel _cameraLogicModel;
        private LevelFinishSequence _finishSequence;
        private UIController _uiController;

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
            
            _uiController = new UIController(_hudView, _winWindowView);

            _finishSequence = new LevelFinishSequence(
                ServiceLocator.Get<InputService>(),
                new CharacterFinishLevelMovement(
                    _player.transform,
                    _finishFlagView.transform,
                    _finishFlagView.CouchTransform,
                    _finishFlagView.CharacterSprite
                ),
                ServiceLocator.Get<EventBus>(),
                ServiceLocator.Get<TimerSystem>(),
                ServiceLocator.Get<ScoreSystem>(),
                _uiController
            );
            _finishFlagView.OnCharacterEntered += _finishSequence.StartSequence;
            
            ServiceLocator.Get<CoinSystem>().InitializeCoins(_coinsOnLevel);
            
            ServiceLocator.Get<UpdateManager>().Register(_cameraLogicModel);
            ServiceLocator.Get<UpdateManager>().Register(_finishSequence);

            foreach (GoombaView goombaView in _goombaView)
            {
                GoombaController goomba = new GoombaController(goombaView, ServiceLocator.Get<UpdateManager>());
            }
            
            
            ServiceLocator.Get<EventBus>().Publish(new LevelStartEvent());
        }

        private void OnDestroy()
        {
            if (Bootstrap.IsInitialized)
            {
                ServiceLocator.Get<UpdateManager>().Remove(_cameraLogicModel);
                ServiceLocator.Get<UpdateManager>().Remove(_finishSequence);

                _uiController.Dispose();
            }
        }
    }
}