using System.Collections.Generic;

namespace Devdog.General.Editors.GameRules
{
    public interface IGameRule
    {
        string saveName { get; }
        bool ignore { get; set; }
        List<GameRuleIssue> issues { get; }

        void UpdateIssue();
    }
}