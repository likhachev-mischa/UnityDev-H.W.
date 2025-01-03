using Modules;
using Zenject;

namespace SnakeGame
{
    //kiss
    public class GameStarter: IInitializable
    {
        private readonly GameStateController m_gameStateController;
        private readonly IDifficulty m_difficulty;

        public GameStarter(GameStateController gameStateController, IDifficulty difficulty)
        {
            m_gameStateController = gameStateController;
            m_difficulty = difficulty;
        }
        
        public void Initialize()
        {
            m_gameStateController.StartGame();
            m_difficulty.Next(out _);
        }
    }
}