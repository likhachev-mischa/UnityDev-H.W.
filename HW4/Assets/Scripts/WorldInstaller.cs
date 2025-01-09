using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class WorldInstaller: MonoInstaller
    {
        [SerializeField]
        private WorldBounds m_worldBounds;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WorldBounds>().FromInstance(m_worldBounds).AsSingle();
        }
    }
}