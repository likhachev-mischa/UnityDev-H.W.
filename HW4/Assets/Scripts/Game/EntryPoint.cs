using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class EntryPoint: MonoBehaviour
    {
        private GameCycle m_gameCycle;

        [Inject]
        public void Construct(GameCycle gameCycle)
        {
            m_gameCycle = gameCycle;
        }
        
        public void Start()
        {
            m_gameCycle.StartGame();
        }
    }
}