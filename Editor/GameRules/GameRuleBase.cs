using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Editors.GameRules
{
    public abstract class GameRuleBase : IGameRule
    {
        protected GameRuleBase()
        {
            issues = new List<GameRuleIssue>();
        }

        public string saveName => "DEVDOG_RULE_" + GetType().Name;

        public bool ignore
        {
            get => EditorPrefs.GetBool(saveName, false);
            set
            {
                if (value == false)
                    EditorPrefs.DeleteKey(saveName);
                else
                    EditorPrefs.SetBool(saveName, true);
            }
        }

        public List<GameRuleIssue> issues { get; protected set; }
        public abstract void UpdateIssue();


        protected T[] RemoveNullFromArray<T>(T[] values, Object owner)
        {
            var l = values.ToList();
            l.RemoveAll(o => o == null);

            if (owner != null) UnityEditor.EditorUtility.SetDirty(owner);

            return l.ToArray();
        }

        protected void SelectObject(Object compTemp)
        {
            Selection.activeObject = compTemp;
        }

        protected void SelectObject(Object compTemp, FieldInfo fieldTemp)
        {
            Selection.activeObject = compTemp;

            // Throws an error in Unity 5.3.5f1
            // Highlighter.Highlight("Inspector", ObjectNames.NicifyVariableName(fieldTemp.Name));
        }

        protected void CreateIssueIfMissingComponent<T>(GameObject obj, string message, MessageType type)
            where T : Component
        {
            if (obj.GetComponent<T>() == null)
                issues.Add(new GameRuleIssue(message, type, new GameRuleAction("Fix (add)",
                    () => { obj.AddComponent<T>(); }
                ), new GameRuleAction("Select", () => { SelectObject(obj); })));
        }
    }
}