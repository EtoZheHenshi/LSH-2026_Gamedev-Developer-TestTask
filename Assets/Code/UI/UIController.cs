using UnityEngine;

namespace Code.UI
{
    public sealed class UIController
    {
        public void Activate()
        {
            Time.timeScale = 0;
        }
    }
}