using UnityEngine;

namespace Code.Gameplay.General.LevelFinishLogic
{
    public sealed class CharacterFinishLevelMovement
    {
        private readonly Transform _playerTransform;
        private readonly Transform _flagTransform;
        private readonly Transform _couchTransform;
        private readonly Transform _characterSprite;
        
        private readonly float _speed = 3f;
        private bool _ready;
        
        public bool Complete { get; private set; }

        public CharacterFinishLevelMovement(Transform playerTransform, Transform flagTransform, Transform couchTransform,
            Transform characterSprite)
        {
            _playerTransform = playerTransform;
            _flagTransform = flagTransform;
            _couchTransform = couchTransform;
            _characterSprite = characterSprite;
        }

        public void Tick(float deltaTime)
        {
            if (Complete || !_ready) return;
            
            if (_characterSprite.position.y > _flagTransform.position.y + 0.5f && _characterSprite.position.x < _couchTransform.position.x)
            {
                _characterSprite.position = new Vector2(_flagTransform.position.x, _characterSprite.position.y);
                _characterSprite.Translate(Vector3.down * (_speed * deltaTime));
            }
            
            if (_characterSprite.position.x < _couchTransform.position.x && _characterSprite.position.y <= _flagTransform.position.y + 0.5f)
            {
                _characterSprite.position = new Vector2(_characterSprite.position.x, _flagTransform.position.y + 0.5f);
                _characterSprite.Translate(Vector3.right * (_speed * deltaTime));
            }
            
            if (_characterSprite.position.y < _couchTransform.position.y + 1f && _characterSprite.position.x >= +_couchTransform.position.x)
            {
                _characterSprite.position = new Vector2(_couchTransform.position.x, _characterSprite.position.y);
                _characterSprite.Translate(Vector3.up * (_speed * deltaTime));
            }

            if (_characterSprite.position.y >= _couchTransform.position.y + 1f && _characterSprite.position.x >= +_couchTransform.position.x)
            {
                _characterSprite.position = new Vector2(_couchTransform.position.x, _couchTransform.position.y + 1f);
                Complete = true;
            }
        }
        
        public void GetReady()
        {
            _characterSprite.position = _playerTransform.position;
            _characterSprite.gameObject.SetActive(true);
            _playerTransform.position = new Vector2(_playerTransform.position.x, _playerTransform.position.y + 30f);
            _ready = true;
        }
    }
}