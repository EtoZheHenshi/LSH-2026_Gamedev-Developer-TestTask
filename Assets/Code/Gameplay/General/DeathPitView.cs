using UnityEngine;

namespace Code.Gameplay.General
{
    public sealed class DeathPitView : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                RestartLevel.Restart(false);
            }
        }
    }
}