using System;
using Code.Gameplay.General;
using UnityEngine;

namespace Code.Gameplay.Enemies.Types.Goomba
{
    public sealed class GoombaView : MonoBehaviour
    {
        [SerializeField] private GoombaConfig _config;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private CircleLayerCheckView _groundLayerCheck;
        
        public event Action<Collision2D> OnCollisionEnterEvent;
        
        public GoombaConfig Config => _config;
        public Rigidbody2D Rigidbody => _rigidbody;
        public CircleLayerCheckView GroundLayerCheck => _groundLayerCheck;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnterEvent?.Invoke(collision);
        }
    }
}