using UnityEditor;

namespace Devdog.General.Editors.GameRules
{
    public class GameRuleIssue
    {
        public GameRuleAction[] actions;
        public string message;
        public MessageType messageType;

        public GameRuleIssue(string message, MessageType messageType, params GameRuleAction[] actions)
        {
            this.message = message;
            this.messageType = messageType;
            this.actions = actions;
        }
    }
}