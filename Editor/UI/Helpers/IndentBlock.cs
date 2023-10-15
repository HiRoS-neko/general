using System;
using UnityEngine;

namespace Devdog.General.Editors
{
    public class IndentBlock : IDisposable
    {
        private const int IndentationPixels = 15;
        public Rect rect;

        public IndentBlock(Rect rect)
        {
            this.rect = rect;
            this.rect.x += IndentationPixels;
            this.rect.width -= IndentationPixels;
        }

        public void Dispose()
        {
            rect.x -= IndentationPixels;
            rect.width += IndentationPixels;
        }
    }
}