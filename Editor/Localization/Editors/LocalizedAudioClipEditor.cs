using UnityEditor;
using UnityEngine;

namespace Devdog.General.Localization.Editors
{
    [CustomPropertyDrawer(typeof(LocalizedAudioClip), true)]
    public class LocalizedAudioClipEditor : LocalizedObjectEditorBase<AudioClip, LocalizedAudioClip>
    {
    }
}