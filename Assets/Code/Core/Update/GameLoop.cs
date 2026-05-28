using Code.Core.Services;
using UnityEngine;

namespace Code.Core.Update
{
    public sealed class GameLoop : MonoBehaviour
    {
        private UpdateManager _updateManager;

        private void Start()
        {
            _updateManager = ServiceLocator.Get<UpdateManager>();
        }

        private void Update()
        {
            _updateManager.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _updateManager.FixedTick(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            _updateManager.LateTick(Time.deltaTime);
        }
    }
}