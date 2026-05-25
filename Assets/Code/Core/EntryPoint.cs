using System;
using Code.Core.Services;
using Code.Core.Update;
using Code.Gameplay.Enemies.Types.Goomba;
using UnityEngine;

namespace Code.Core
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GoombaView _goombaView;

        private void Start()
        {
            GoombaController goomba = new GoombaController(_goombaView, ServiceLocator.Get<UpdateManager>());
        }
    }
}