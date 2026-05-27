using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class BoxLayerCheckView : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private LayerMask _checkLayers;
        
        public Vector2 Size => _size;
        public LayerMask CheckLayers => _checkLayers;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _size);
        }
    }
}