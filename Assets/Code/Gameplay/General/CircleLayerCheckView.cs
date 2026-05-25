using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class CircleLayerCheckView : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.1f;
        [SerializeField] private LayerMask _checkLayers;
        
        public float Radius => _radius;
        public LayerMask CheckLayers => _checkLayers;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}