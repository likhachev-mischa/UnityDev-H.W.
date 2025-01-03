using System.Collections.Generic;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class CoinSpawner : MonoMemoryPool<Coin>, ICoinSpawner
    {
        public List<ICoin> Coins { get; } = new();

        private IWorldBounds m_worldBounds;

        [Inject]
        public void Construct(IWorldBounds worldBounds)
        {
            m_worldBounds = worldBounds;
        }

        ICoin ICoinSpawner.Spawn()
        {
            return Spawn();
        }

        void ICoinSpawner.Despawn(ICoin coin)
        {
            Despawn((Coin)coin);
        }
        
        bool ICoinSpawner.IsEmpty()
        {
            return Coins.Count == 0;
        }

        protected override void Reinitialize(Coin item)
        {
            item.Generate();
            SetPosition(item);
        }

        protected override void OnSpawned(Coin item)
        {
            base.OnSpawned(item);
            Coins.Add(item);
        }

        protected override void OnDespawned(Coin item)
        {
            base.OnDespawned(item);
            Coins.Remove(item);
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