using Zenject;

namespace SnakeGame
{
    public class SystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InputInstaller.Install(Container);
            GameInstaller.Install(Container);
            ScoreInstaller.Install(Container);
        }
    }
}