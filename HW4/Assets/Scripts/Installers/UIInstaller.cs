using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class UIInstaller: MonoInstaller
    {
        [SerializeField]
        private GameUI m_gameUI;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameUI>().FromInstance(m_gameUI).AsSingle(); 
        }   
    }
}