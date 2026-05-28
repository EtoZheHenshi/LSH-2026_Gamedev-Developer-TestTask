using Code.Core.Update;
using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class CameraLogicModel : ILateTickable
    {
        private readonly Transform _cameraTarget;
        private readonly Collider2D _characterStopCollider;
        private readonly Camera _camera;
        private readonly float _halfCameraWidth;
        private readonly float _cameraOffset = 3f;

        public CameraLogicModel(Transform cameraTarget, Collider2D characterStopCollider, Camera mainCamera)
        {
            _cameraTarget = cameraTarget;
            _characterStopCollider = characterStopCollider;
            _camera = mainCamera;
            
            _halfCameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        }

        public void LateTick(float deltaTime)
        {
            Vector3 position = _camera.transform.position;

            if (_cameraTarget.position.x > position.x - _cameraOffset)
            {
                position.x = _cameraTarget.position.x + _cameraOffset;
                
                _characterStopCollider.transform.position = new Vector3(
                    position.x - _halfCameraWidth,
                    _characterStopCollider.transform.position.y,
                    _characterStopCollider.transform.position.z);
            }
            
            _camera.transform.position = position;
        }
    }
}