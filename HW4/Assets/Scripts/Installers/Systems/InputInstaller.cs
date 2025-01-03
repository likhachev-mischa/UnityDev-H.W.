using Zenject;

namespace SnakeGame
{
    public class InputInstaller: Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SnakeMoveController>().AsSingle().NonLazy();
        }
    }
}