using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class DifficultyInstaller : MonoInstaller
    {
        [SerializeField]
        private int m_maxDifficulty;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Difficulty>().AsSingle().WithArguments(m_maxDifficulty).NonLazy();
            Container.BindInterfacesTo<DifficultyController>().AsSingle().NonLazy();
        }
    }
}