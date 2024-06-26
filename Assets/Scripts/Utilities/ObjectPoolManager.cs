
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private List<ObjectPoolsInfo> objectPools;
        private static ObjectPoolManager instance;

        public static ObjectPoolManager Instance { get => instance; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            objectPools = new List<ObjectPoolsInfo>();
        }


        public GameObject SpawnObject(GameObject prefab)
        {
            ObjectPoolsInfo pool = null;
            foreach (var p in objectPools)
            {
                if (p.Name == prefab.tag)
                {
                    pool = p;
                    break;
                }
            }
            if (pool == null)
            {
                pool = new ObjectPoolsInfo();
                pool.Name = prefab.tag;
                pool.InactiveObjects = new List<GameObject>();
                objectPools.Add(pool);
            }

            GameObject obj = null;

            if (pool.InactiveObjects.Count > 0)
            {
                obj = pool.InactiveObjects[0];
                obj.SetActive(true);
                pool.InactiveObjects.RemoveAt(0);
            }
            else
            {
                obj = GameObject.Instantiate(prefab);
                obj.transform.position = Vector3.zero;
                obj.transform.rotation = Quaternion.identity;
            }
            return obj;
        }

        public T SpawnObject<T>(Component component)
        {
            ObjectPoolsInfo pool = null;
            foreach (var p in objectPools)
            {
                if (p.Name == component.tag)
                {
                    pool = p;
                    break;
                }
            }
            if (pool == null)
            {
                pool = new ObjectPoolsInfo();
                pool.Name = component.tag;
                pool.InactiveObjects = new List<GameObject>();
                objectPools.Add(pool);
            }

            GameObject obj = null;

            if (pool.InactiveObjects.Count > 0)
            {
                obj = pool.InactiveObjects[0];
                obj.SetActive(true);
                pool.InactiveObjects.RemoveAt(0);
            }
            else
            {
                obj = GameObject.Instantiate(component.gameObject);
                obj.transform.position = Vector3.zero;
                obj.transform.rotation = Quaternion.identity;
            }
            return obj.GetComponent<T>();
        }

        public void DeSpawnObject(GameObject objectToDespawn)
        {
            ObjectPoolsInfo pool = null;
            foreach (var p in objectPools)
            {
                if (p.Name == objectToDespawn.tag)
                {
                    pool = p;
                    break;
                }
            }
            if (pool == null)
            {
                Debug.Log("pool does not exist");
                return;
            }
            ResetObject(pool, objectToDespawn);
        }

        private void ResetObject(ObjectPoolsInfo pool, GameObject objectToDespawn)
        {
            objectToDespawn.transform.position = Vector3.zero;
            objectToDespawn.transform.rotation = Quaternion.identity;
            objectToDespawn.SetActive(false);
            pool.InactiveObjects.Add(objectToDespawn);
        }

        //public void DeSpawnObjectWithDelay(GameObject obj, float delay)
        //{
        //    StartCoroutine(DelayInDeSpawn(obj, delay));
        //}

        //IEnumerator DelayInDeSpawn(GameObject obj, float time)
        //{
        //    yield return new WaitForSeconds(time);
        //    DeSpawnObject(obj);
        //}
    }

    public class ObjectPoolsInfo
    {
        public string Name;
        public List<GameObject> InactiveObjects;
    }
}