using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class GroundedCheckView : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.1f;
        [SerializeField] private LayerMask _groundLayers;
        
        public float Radius => _radius;
        public LayerMask GroundLayers => _groundLayers;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}