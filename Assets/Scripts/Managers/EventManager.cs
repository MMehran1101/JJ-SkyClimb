using System;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        public static event Action<int> onScoreChanged;

        public static void ScoreChanged(int newScore)
        {
            onScoreChanged?.Invoke(newScore);
        }
    }
}