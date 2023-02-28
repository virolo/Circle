using DefaultNamespace.Extensions;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreWindow : MonoBehaviour
    {
        [SerializeField]
        private GameObject _root;

        [SerializeField]
        private TextMeshProUGUI _scoreMesh;

        private IGameLogic _gameLogic;
        
        public void Init(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
            _gameLogic.OnGameFinished += ShowFinalScore;
            _gameLogic.OnReset += RestartGame;
        }

        private void OnDestroy()
        {
            _gameLogic.OnGameFinished -= ShowFinalScore;
            _gameLogic.OnReset -= RestartGame;
        }

        private void RestartGame()
        {
            HideScore();
        }

        private void ShowFinalScore(PlayerGameData playerGameData)
        {
            SetScore(playerGameData);
            _root.Enable();
        }

        private void HideScore()
        {
            _root.Disable();
        }
        
        private void SetScore(PlayerGameData playerGameData)
        {
            _scoreMesh.text = playerGameData.Score.ToString();
        }
    }
}