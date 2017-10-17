using UnityEngine;

namespace NG.Patterns.Structure.ObjectPool
{
    [RequireComponent(typeof(BulletPool))]
    public class BulletShooter : MonoBehaviour
    {
        [SerializeField]
        private float bulletInitialSpeed = 5f;

        [SerializeField]
        private float shotsPerSecond = 10f;

        [SerializeField]
        private BulletCatcher bulletCather;

        private BulletPool bulletPool;

        private float lastShotTime = 0f;

        private void Awake()
        {
            bulletPool = GetComponent<BulletPool>();

            bulletCather.OnCaughtBullet += OnCaughtBullet;
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.Space) && Time.time - lastShotTime > 1 / shotsPerSecond)
            {
                Fire();
            }

            if(Input.GetKeyDown(KeyCode.A))
            {
                transform.position -= Vector3.right;
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                transform.position += Vector3.right;
            }
        }

        private void Fire()
        {
            Rigidbody bullet = bulletPool.GetBullet(transform.position);

            if (bullet != null)
            {
                bullet.velocity = transform.forward * bulletInitialSpeed;

                lastShotTime = Time.time;
            }
        }

        private void OnCaughtBullet(Rigidbody caughtBullet)
        {
            bulletPool.ReturnToPool(caughtBullet);
        }
    }
}
