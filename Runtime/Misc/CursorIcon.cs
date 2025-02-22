﻿using System;
using UnityEngine;

namespace Devdog.General
{
    [Serializable]
    public struct CursorIcon : IEquatable<CursorIcon>
    {
        public Texture2D texture;
        public Vector2 hotspot;
        public CursorMode cursorMode;

        public CursorIcon(Texture2D texture, Vector2 hotspot, CursorMode cursorMode = CursorMode.Auto)
        {
            this.texture = texture;
            this.hotspot = hotspot;
            this.cursorMode = cursorMode;
        }

        public static CursorIcon current { get; private set; }

        public bool Equals(CursorIcon other)
        {
            return other.texture == texture &&
                   other.hotspot == hotspot &&
                   other.cursorMode == cursorMode;
        }

        public void Enable()
        {
            if (Equals(current) == false) Cursor.SetCursor(texture, hotspot, cursorMode);

            current = this;
        }
    }
}