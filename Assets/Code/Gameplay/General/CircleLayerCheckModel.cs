using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class CircleLayerCheckModel
    {
        private readonly float _radius;
        private readonly LayerMask _checkLayers;
        private readonly Transform _checkTransform;
        
        public bool IsCheckTrue { get; private set; }

        public CircleLayerCheckModel(float radius, LayerMask checkLayers, Transform checkTransform)
        {
            _radius = radius;
            _checkLayers = checkLayers;
            _checkTransform = checkTransform;
        }

        public CircleLayerCheckModel(CircleLayerCheckView view) : this(view.Radius, view.CheckLayers, view.transform)
        {
        }

        public void Tick()
        {
            IsCheckTrue = Physics2D.OverlapCircle(_checkTransform.position, _radius, _checkLayers);
        }
    }
}