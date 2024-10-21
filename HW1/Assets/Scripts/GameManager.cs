using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        public void FinishGame()
        {
            Time.timeScale = 0;
            Debug.Log("Game finished!");
        }
    }
}