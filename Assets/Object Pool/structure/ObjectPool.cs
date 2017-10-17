using UnityEngine;
using System.Collections.Generic;

namespace NG.Patterns.Structure.ObjectPool
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : UnityEngine.Object
    {
        [SerializeField]
        private int initialPoolSize;

        [SerializeField]
        private int maxPoolSize;

        [SerializeField]
        private T objectPrefab;

        private List<T> pooledObjects;
        private Queue<T> freeObjects;

        private void OnValidate()
        {
            if(maxPoolSize < initialPoolSize)
            {
                maxPoolSize = initialPoolSize;
            }
        }

        protected virtual void Awake()
        {
            pooledObjects = new List<T>(initialPoolSize);
            freeObjects = new Queue<T>(initialPoolSize);

            SpawnObjects(initialPoolSize);
        }

        protected virtual void SpawnObjects(int objectsToSpawn)
        {
            for(int i = 0; i < objectsToSpawn; i++)
            {
                if(pooledObjects.Count < maxPoolSize)
                {
                    T newObject = Instantiate(objectPrefab);

                    AddObject(newObject);
                }
            }
        }

        private void AddObject(T objectToAdd)
        {
            pooledObjects.Add(objectToAdd);

            ReturnToPool(objectToAdd);
        }

        public virtual void ReturnToPool(T objectToReturn)
        {
            if (!pooledObjects.Contains(objectToReturn))
            {
                Debug.LogError(objectToReturn.name + " doesn't belong to Pool");

                return;
            }

            freeObjects.Enqueue(objectToReturn);
        }

        public virtual T GetObject()
        {
            T objectFromPool = null;

            if (freeObjects.Count == 0)
            {
                Debug.LogWarning("No free objects in the pool");
            }
            else
            {
                objectFromPool = freeObjects.Dequeue();
            }

            return objectFromPool;
        }
    }
}
