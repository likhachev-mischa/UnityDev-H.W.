using Modules;
using Zenject;

namespace SnakeGame
{
    public class GameStateInstaller: Installer<GameStateInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Score>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle().NonLazy();
        }
    }
}