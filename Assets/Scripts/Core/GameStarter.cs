using System;
using UnityEngine;

namespace StarDef.Core
{
    public class GameStarter : MonoBehaviour
    {
        public Action OnGameStart;

        public void StartGame() { if (OnGameStart != null) { OnGameStart(); } }
    }
}