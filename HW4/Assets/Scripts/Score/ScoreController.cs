using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class ScoreController: IInitializable, IDisposable
    {
        private IScore m_score;
        private ICoinSpawner m_coinSpawner;

        public ScoreController(IScore score, ICoinSpawner coinSpawner)
        {
            m_score = score;
            m_coinSpawner = coinSpawner;
        }

        void IInitializable.Initialize()
        {
            m_coinSpawner.CoinDespawned += OnCoinDespawned;
        }

        void IDisposable.Dispose()
        {
            m_coinSpawner.CoinDespawned -= OnCoinDespawned;
        }

        private void OnCoinDespawned(ICoin obj)
        {
            m_score.Add(obj.Score); 
        }
    }
}