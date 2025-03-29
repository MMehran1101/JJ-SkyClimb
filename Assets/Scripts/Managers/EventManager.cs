using System;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        public static event Action<int> onScoreChanged;
        public static event Action onGameOver;

        public static void ScoreChanged(int newScore)
        {
            onScoreChanged?.Invoke(newScore);
        }

        public static void GameOver()
        {
            onGameOver?.Invoke();
        }
    }
}