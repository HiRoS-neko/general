using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.General
{
    public class IgnoreRangeTriggerRangeHandler : MonoBehaviour, ITriggerRangeHandler
    {
        public IEnumerable<Player> GetPlayersInRange()
        {
            return Array.Empty<Player>();
        }

        public bool IsPlayerInRange(Player target)
        {
            return true;
        }
    }
}