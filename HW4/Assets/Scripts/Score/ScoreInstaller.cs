using Modules;
using Zenject;

namespace SnakeGame
{
    public class ScoreInstaller: Installer<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Score>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreController>().AsSingle().NonLazy();
        }
    }
}