using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Devdog.General.Editors.GameRules
{
    public class ReplacedByAttributeRule : GameRuleBase
    {
        // TODO: Check if this is working...
        public override void UpdateIssue()
        {
            var types = ReflectionUtility.GetAllClassesWithAttribute(typeof(ReplacedByAttribute));
            foreach (var currentType in types)
            {
                var newComponentType =
                    (ReplacedByAttribute)currentType.GetCustomAttributes(typeof(ReplacedByAttribute), true).First();
                if (typeof(Component).IsAssignableFrom(currentType))
                {
                    var components = Resources.FindObjectsOfTypeAll(currentType).Cast<Component>().ToArray();
                    foreach (var component in components)
                    {
                        try
                        {
                            var tempComponent = component;
                            var tempNewType = newComponentType;
                            issues.Add(new GameRuleIssue("Deprecated type " + tempComponent.GetType() + " is used",
                                MessageType.Error, new GameRuleAction("Fix (replace)",
                                    () =>
                                    {
                                        if (tempComponent != null && tempComponent.gameObject != null)
                                        {
                                            var newComponent = tempComponent.gameObject.AddComponent(tempNewType.type);
                                            ReflectionUtility.CopySerializableValues(tempComponent, newComponent);
                                            Object.DestroyImmediate(tempComponent, true);
                                        }
                                    }), new GameRuleAction("Select object", () => { SelectObject(tempComponent); })));
                        }
                        catch (Exception)
                        {
                            // Ignored
                        }

                        UnityEditor.EditorUtility.SetDirty(component);
                    }
                }
            }
        }
    }
}