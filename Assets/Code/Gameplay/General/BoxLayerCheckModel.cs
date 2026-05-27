using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class BoxLayerCheckModel
    {
        private readonly Vector2 _size;
        private readonly LayerMask _checkLayers;
        private readonly Transform _checkTransform;
        
        public bool IsCheckTrue { get; private set; }

        public BoxLayerCheckModel(Vector2 size, LayerMask checkLayers, Transform checkTransform)
        {
            _size = size;
            _checkLayers = checkLayers;
            _checkTransform = checkTransform;
        }

        public BoxLayerCheckModel(BoxLayerCheckView view) : this(view.Size, view.CheckLayers, view.transform)
        {
        }

        public void Tick()
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(_checkTransform.position, _size, 0f, _checkLayers);
            IsCheckTrue = false;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].bounds.max.y < _checkTransform.position.y + 0.05f)
                {
                    IsCheckTrue = true;
                    break;
                }
            }
        }
    }
}