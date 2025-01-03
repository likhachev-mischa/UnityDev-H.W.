using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class SystemsInstaller : MonoInstaller
    {
        [SerializeField]
        private int m_maxDifficulty;

        public override void InstallBindings()
        {
            InputInstaller.Install(Container);

            Container.BindInterfacesTo<Difficulty>().AsSingle().WithArguments(m_maxDifficulty).NonLazy();

            GameStateInstaller.Install(Container);
            ObserversInstaller.Install(Container);
            GameUIInstaller.Install(Container);

            Container.BindInterfacesTo<GameStarter>().AsSingle().NonLazy();
        }
    }
}