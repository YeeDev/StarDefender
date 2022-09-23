using System;
using UnityEngine;

namespace StarDef.Core
{
    public class ControlEnabler : MonoBehaviour
    {
        public Action<bool> OnControlChange;

        public void StartGame(bool isControlEnabled) { if (OnControlChange != null) { OnControlChange(isControlEnabled); } }
    }
}