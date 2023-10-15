using System;
using UnityEngine;

namespace Devdog.General.Editors
{
    public class ColorBlock : IDisposable
    {
        private readonly bool _active;
        private readonly Color _before;

        public ColorBlock(Color color)
            : this(color, true)
        {
        }

        public ColorBlock(Color color, bool active)
        {
            _before = GUI.color;
            _active = active;

            if (_active) GUI.color = color;
        }


        public void Dispose()
        {
            if (_active) GUI.color = _before;
        }
    }
}