using UnityEngine;

namespace DefaultNamespace
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField]
        private GameLogic _gameLogic;

        [SerializeField]
        private ScoreWindow _scoreWindow;

        private void Start()
        {
            _scoreWindow.Init(_gameLogic);
        }
    }
}