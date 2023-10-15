using UnityEditor;
using UnityEngine;

namespace Devdog.General.Localization.Editors
{
    [CustomPropertyDrawer(typeof(LocalizedSprite), true)]
    public class LocalizedSpriteEditor : LocalizedObjectEditorBase<Sprite, LocalizedSprite>
    {
    }
}