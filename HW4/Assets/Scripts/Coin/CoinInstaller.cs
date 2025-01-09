using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class CoinInstaller: MonoInstaller
    {
        [SerializeField]
        private Coin m_coinPrefab;
        
        [SerializeField]
        private Transform m_parent;

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Coin, MonoMemoryPool<Coin>>().FromComponentInNewPrefab(m_coinPrefab)
                .UnderTransform(m_parent).AsSingle();

            Container.BindInterfacesTo<CoinManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CoinSpawnController>().AsSingle().NonLazy();
        }
    }
}