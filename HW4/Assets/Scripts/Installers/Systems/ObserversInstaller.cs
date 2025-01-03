using Zenject;

namespace SnakeGame
{
    public class ObserversInstaller: Installer<ObserversInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SnakeCollisionObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SnakeCollectCoinObserver>().AsSingle().NonLazy();

            Container.BindInterfacesTo<DifficultyChangeObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CoinCollectObserver>().AsSingle().NonLazy();
        }
    }
}