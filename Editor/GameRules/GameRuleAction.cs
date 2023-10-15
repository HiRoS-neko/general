using System;

namespace Devdog.General.Editors.GameRules
{
    public class GameRuleAction
    {
        public Action action;
        public string name;

        public GameRuleAction(string name, Action action)
        {
            this.name = name;
            this.action = action;
        }
    }
}