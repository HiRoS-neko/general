using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.General
{
    public class TriggerRangeHandler2D : MonoBehaviour, ITriggerRangeHandler
    {
        [SerializeField]
        private float _useRange = 10f;

        private readonly List<Player> _playersInRange = new();
        private CircleCollider2D _circleCollider;
        private Rigidbody2D _rigidbody2D;
        private TriggerBase _trigger;

        public float useRange => _useRange;

        protected void Awake()
        {
            _trigger = GetComponentInParent<TriggerBase>();
            Assert.IsNotNull(_trigger, "TriggerRangeHandler used but no TriggerBase found on object.");

            _rigidbody2D = gameObject.GetOrAddComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;

            _circleCollider = gameObject.GetOrAddComponent<CircleCollider2D>();
            _circleCollider.isTrigger = true;
            _circleCollider.radius = useRange;

            gameObject.layer = 2; // Ignore raycasts
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<Player2D>();
            if (player != null)
            {
                _playersInRange.Add(player);
                _trigger.NotifyCameInRange(player);
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<Player2D>();
            if (player != null)
            {
                _trigger.NotifyWentOutOfRange(player);
                _playersInRange.Remove(player);
            }
        }

        public IEnumerable<Player> GetPlayersInRange()
        {
            return _playersInRange;
        }

        public bool IsPlayerInRange(Player player)
        {
            return _playersInRange.Contains(player);
        }
    }
}