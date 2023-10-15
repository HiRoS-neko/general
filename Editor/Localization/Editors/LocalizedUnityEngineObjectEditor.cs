using UnityEditor;
using UnityEngine;

namespace Devdog.General.Localization.Editors
{
    [CustomPropertyDrawer(typeof(LocalizedObject), true)]
    public class LocalizedUnityEngineObjectEditor : LocalizedObjectEditorBase<Object, LocalizedObject>
    {
    }
}