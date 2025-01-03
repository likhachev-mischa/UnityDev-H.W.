using Zenject;

namespace SnakeGame
{
    public class GameUIInstaller: Installer<GameUIInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameUIController>().AsSingle().NonLazy();
        }
    }
}