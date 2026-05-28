using System;
using Code.Gameplay.Enemies.Behaviours;
using Code.Gameplay.General;
using UnityEngine;

namespace Code.Gameplay.Enemies.Types.Goomba
{
    public sealed class GoombaView : MonoBehaviour
    {
        [SerializeField] private GoombaConfig _config;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private BoxLayerCheckView _groundLayerCheck;
        [SerializeField] private StompableView _stompableView;
        
        public event Action<Collision2D> OnCollisionEnterEvent;
        public event Action OnStomp
        {
            add => _stompableView.OnStomp += value;
            remove => _stompableView.OnStomp -= value;
        }
        public event Action OnDestroyEvent;
        
        public GoombaConfig Config => _config;
        public Rigidbody2D Rigidbody => _rigidbody;
        public BoxLayerCheckView GroundLayerCheck => _groundLayerCheck;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnterEvent?.Invoke(collision);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke();
        }
    }
}