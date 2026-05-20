using Code.Core.Update;
using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class GroundedCheckModel
    {
        private readonly float _radius;
        private readonly LayerMask _groundLayers;
        private readonly Transform _checkTransform;
        
        public bool IsGrounded { get; private set; }

        public GroundedCheckModel(float radius, LayerMask groundLayers, Transform checkTransform)
        {
            _radius = radius;
            _groundLayers = groundLayers;
            _checkTransform = checkTransform;
        }

        public void Tick()
        {
            IsGrounded = Physics2D.OverlapCircle(_checkTransform.position, _radius, _groundLayers);
        }
    }
}