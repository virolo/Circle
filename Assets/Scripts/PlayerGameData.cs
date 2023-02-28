using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerGameData
    {
        public int Score { get; private set; }

        public PlayerGameData()
        {
            Score = 0;
        }

        public void RestartData()
        {
            Score = 0;
        }
        
        public void AddScore(int score)
        {
            Debug.Log($"Added score: {score} - Total score: {Score}");
            Score += score;
        }
    }
}