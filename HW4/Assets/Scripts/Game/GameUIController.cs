using System;
using Modules;
using Zenject;

namespace SnakeGame
{
    public class GameUIController : IInitializable, IDisposable
    {
        private readonly IGameUI m_gameUI;

        private readonly GameStateController m_gameStateController;

        private readonly IScore m_score;
        private readonly IDifficulty m_difficulty;

        public GameUIController(IGameUI gameUI, GameStateController gameStateController, IScore score,
            IDifficulty difficulty)
        {
            m_gameUI = gameUI;
            m_gameStateController = gameStateController;
            m_score = score;
            m_difficulty = difficulty;
        }

        void IInitializable.Initialize()
        {
            m_gameStateController.GameStarted += OnGameStarted;
            m_gameStateController.GameWon += OnGameWon;
            m_gameStateController.GameLost += OnGameLost;
            
            m_score.OnStateChanged += OnScoreChanged;
            m_difficulty.OnStateChanged += OnDifficultyChanged;
        }

        void IDisposable.Dispose()
        {
            m_gameStateController.GameStarted -= OnGameStarted;
            m_gameStateController.GameWon -= OnGameWon;
            m_gameStateController.GameLost -= OnGameLost;
            
            m_score.OnStateChanged -= OnScoreChanged;
            m_difficulty.OnStateChanged -= OnDifficultyChanged;
        }

        private void OnGameStarted()
        {
            m_gameUI.SetScore(m_score.Current.ToString());
            m_gameUI.SetDifficulty(m_difficulty.Current, m_difficulty.Max);
        }
        
        private void OnGameWon()
        {
            m_gameUI.GameOver(true);
        }
        
        private void OnGameLost()
        {
            m_gameUI.GameOver(false);
        }
        
        private void OnScoreChanged(int obj)
        {
            m_gameUI.SetScore(m_score.Current.ToString());
        }
        
        private void OnDifficultyChanged()
        {
            m_gameUI.SetDifficulty(m_difficulty.Current, m_difficulty.Max);
        }
    }
}