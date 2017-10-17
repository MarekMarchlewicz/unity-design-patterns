using UnityEngine;

namespace NG.Patterns.Structure.ObjectPool
{
    public class BulletCatcher : MonoBehaviour
    {
        public System.Action<Rigidbody> OnCaughtBullet;

        private void OnTriggerEnter(Collider collider)
        {
            if(OnCaughtBullet != null)
            {
                OnCaughtBullet(collider.attachedRigidbody);
            }
        }
    }
}
