using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class GameLogic : MonoBehaviour, IGameLogic
    {
        public event Action OnReset;
        
        public event Action<PlayerGameData> OnGameFinished;
        
        public event Action OnGameStarted;
        
        [SerializeField]
        private Timer _startCountdown;

        [SerializeField]
        private Timer _gameCountdown;

        [SerializeField]
        private Circle _circle;

        [SerializeField]
        private Button _restartButton;

        [SerializeField]
        private Button _homeButton;

        private PlayerGameData _playerGameData;

        public void Awake()
        {
            _playerGameData = new PlayerGameData();
            StartGameCountdown();
        }

        private void CirclePressed()
        {
            AddScore();
            RepositionCircleToRandomPosition();
        }

        private void StartGameCountdown()
        {
            _startCountdown.OnFinished += StartGame;
            _startCountdown.StartCounter();
        }

        private void StartGame()
        {
            _startCountdown.OnFinished -= StartGame;
            _startCountdown.Disable();
            _circle.OnPressed += CirclePressed;
            _gameCountdown.OnFinished += GameFinished;
            _gameCountdown.StartCounter();
            RepositionCircleToRandomPosition();
            OnGameStarted?.Invoke();
        }

        private void RepositionCircleToRandomPosition()
        {
            _circle.Enable();
            _circle.SetRandomPosition();
        }

        private void RestartGame()
        {
            _playerGameData.RestartData();
            DisableScoreWindowButtons();
            _gameCountdown.Disable();
            StartGameCountdown();
            OnReset?.Invoke();
        }

        private void AddScore()
        {
            _playerGameData.AddScore(1);
        }

        private void GameFinished()
        {
            EnableScoreWindowButtons();
            _gameCountdown.OnFinished -= GameFinished;
            _circle.OnPressed -= CirclePressed;
            _circle.Disable();
            OnGameFinished?.Invoke(_playerGameData);
        }

        private void EnableScoreWindowButtons()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _homeButton.onClick.AddListener(LoadMainMenu);
        }

        private void DisableScoreWindowButtons()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _homeButton.onClick.RemoveListener(LoadMainMenu);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}