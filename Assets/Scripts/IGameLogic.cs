using System;

namespace DefaultNamespace
{
    public interface IGameLogic
    {
        public event Action OnReset;

        public event Action<PlayerGameData> OnGameFinished;

        public event Action OnGameStarted;
    }
}