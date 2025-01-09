using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SnakeInstaller: MonoInstaller
    {
        [SerializeField]
        private Snake m_snake;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Snake>().FromInstance(m_snake).AsSingle().NonLazy();

            Container.BindInterfacesTo<SnakeCollectCoinObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SnakeCollisionObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SnakeExpandController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SnakeSpeedController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SnakeTurnController>().AsSingle().NonLazy();
        }
    }
}