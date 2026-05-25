using UnityEngine;

namespace Code.Gameplay.Enemies.Types.Goomba
{
    [CreateAssetMenu(menuName = "Configs/Enemies/Configs/Goomba", fileName = "GoombaConfig")]
    public sealed class GoombaConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravityModifier;
        
        public float Speed => _speed;
        public float GravityModifier => _gravityModifier;
    }
}