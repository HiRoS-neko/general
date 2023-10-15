using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Devdog.General
{
    public class TriggerRangeHandler : MonoBehaviour, ITriggerRangeHandler
    {
        [SerializeField]
        private float _useRange = 10f;

        private readonly List<Player> _playersInRange = new();
        private Rigidbody _rigidbody;
        private SphereCollider _sphereCollider;
        private TriggerBase _trigger;

        public float useRange => _useRange;

        protected void Awake()
        {
            _trigger = GetComponentInParent<TriggerBase>();
            Assert.IsNotNull(_trigger, "TriggerRangeHandler used but no TriggerBase found on object.");

            _rigidbody = gameObject.GetOrAddComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;

            _sphereCollider = gameObject.GetOrAddComponent<SphereCollider>();
            _sphereCollider.isTrigger = true;
            _sphereCollider.radius = useRange;

            gameObject.layer = 2; // Ignore raycasts
        }

        protected void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _playersInRange.Add(player);
                player.triggerHandler.triggersInRange.Add(_trigger);
                _trigger.NotifyCameInRange(player);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                _trigger.NotifyWentOutOfRange(player);
                player.triggerHandler.triggersInRange.Remove(_trigger);
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