using UnityEngine;

namespace Devdog.General
{
    public class PositionAtPlayer : MonoBehaviour
    {
        public Vector3 offset;
        public bool continuous;

        protected void LateUpdate()
        {
            if (continuous) PositionNow();
        }


        protected void OnEnable()
        {
            PositionNow();
        }

        private void PositionNow()
        {
            if (PlayerManager.instance == null || PlayerManager.instance.currentPlayer == null) return;

            transform.position = PlayerManager.instance.currentPlayer.transform.position;
        }
    }
}