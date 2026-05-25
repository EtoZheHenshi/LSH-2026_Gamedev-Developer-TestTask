using Code.Gameplay.General;
using UnityEngine;

namespace Code.Gameplay.Enemies.Types.Goomba
{
    public sealed class GoombaView : MonoBehaviour
    {
        [SerializeField] private GoombaConfig _config;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private CircleLayerCheckView _groundLayerCheck;
        [SerializeField] private CircleLayerCheckView _leftWallLayerCheck;
        [SerializeField] private CircleLayerCheckView _rightWallLayerCheck;
        
        public GoombaConfig Config => _config;
        public Rigidbody2D Rigidbody => _rigidbody;
        public CircleLayerCheckView GroundLayerCheck => _groundLayerCheck;
        public CircleLayerCheckView LeftWallLayerCheck => _leftWallLayerCheck;
        public CircleLayerCheckView RightWallLayerCheck => _rightWallLayerCheck;
    }
}