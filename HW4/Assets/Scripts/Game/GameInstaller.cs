using Zenject;

namespace SnakeGame
{
    public class GameInstaller: Installer<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameCycle>().AsSingle().NonLazy();
        }
    }
}