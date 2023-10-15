using System;
using UnityEngine;

namespace Devdog.General.Editors
{
    public class GUIDisabledBlock : IDisposable
    {
        private static int _counter;

        public GUIDisabledBlock()
        {
            if (_counter == 0) GUI.enabled = false;

            _counter++;
        }

        public void Dispose()
        {
            _counter--;

            if (_counter == 0) GUI.enabled = true;
        }
    }
}