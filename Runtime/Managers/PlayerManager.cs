using System;

namespace Devdog.General
{
    public class PlayerManager : ManagerBase<PlayerManager>
    {
        private Player _currentPlayer;

        public Player currentPlayer
        {
            get => _currentPlayer;
            set
            {
                var before = _currentPlayer;
                _currentPlayer = value;
                if (OnPlayerChanged != null) OnPlayerChanged(before, _currentPlayer);
            }
        }

        public event Action<Player, Player> OnPlayerChanged;
    }
}