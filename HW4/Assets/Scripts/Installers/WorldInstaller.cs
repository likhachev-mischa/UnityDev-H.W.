using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class WorldInstaller: MonoInstaller
    {
        [SerializeField]
        private Snake m_snake;

        [SerializeField]
        private WorldBounds m_worldBounds;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Snake>().FromInstance(m_snake).AsSingle();
            Container.BindInterfacesTo<WorldBounds>().FromInstance(m_worldBounds).AsSingle();
        }
    }
}