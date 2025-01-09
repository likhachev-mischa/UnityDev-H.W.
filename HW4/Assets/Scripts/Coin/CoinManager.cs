using System;
using System.Collections.Generic;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class CoinManager : ICoinSpawner
    {
        public event Action<ICoin> CoinDespawned;

        public List<ICoin> Coins { get; } = new();

        private MonoMemoryPool<Coin> m_pool;
        private IWorldBounds m_worldBounds;

        [Inject]
        public void Construct(IWorldBounds worldBounds, MonoMemoryPool<Coin> pool)
        {
            m_worldBounds = worldBounds;
            m_pool = pool;
        }

        ICoin ICoinSpawner.Spawn()
        {
            var coin = m_pool.Spawn();
            Coins.Add(coin);

            coin.Generate();
            SetPosition(coin);

            return coin;
        }

        void ICoinSpawner.Despawn(ICoin coin)
        {
            Coins.Remove(coin);
            m_pool.Despawn((Coin)coin);
            CoinDespawned?.Invoke(coin);
        }

        bool ICoinSpawner.IsEmpty()
        {
            return Coins.Count == 0;
        }

        //possible infinite loop if there are too many coins
        private void SetPosition(Coin item)
        {
            var position = m_worldBounds.GetRandomPosition();
            while (!IsPositionValid(position))
            {
                position = m_worldBounds.GetRandomPosition();
            }

            item.Position = position;
        }

        private bool IsPositionValid(Vector2Int position)
        {
            foreach (var coin in Coins)
            {
                if (coin.Position == position)
                {
                    return false;
                }
            }

            return true;
        }
    }
}