using UnityEngine;

namespace NG.Patterns.Structure.ObjectPool
{
    public class BulletPool : ObjectPool<Rigidbody>
    {
        public override void ReturnToPool(Rigidbody objectToReturn)
        {
            base.ReturnToPool(objectToReturn);

            objectToReturn.gameObject.SetActive(false);
        }
        
        public Rigidbody GetBullet(Vector3 atPosition)
        {
            Rigidbody rigidbodyToReturn = GetObject();

            if (rigidbodyToReturn != null)
            {
                rigidbodyToReturn.transform.position = atPosition;
                rigidbodyToReturn.gameObject.SetActive(true);
            }

            return rigidbodyToReturn;
        }
    }
}
