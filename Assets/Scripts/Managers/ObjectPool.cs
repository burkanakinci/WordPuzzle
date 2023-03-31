using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPool : CustomBehaviour
{
    [Serializable]
    public class Pool
    {
        public string PrefabTag;
        public GameObject PooledPrefab;
        public int SpawnedSize = 1;
        public Transform PrefabParent;
    }
    [SerializeField] private List<Pool> m_Pools = new List<Pool>();
    public Dictionary<string, Queue<IPooledObject>> m_poolDictionary;

    private IPooledObject m_SpawnOnPool;
    private IPooledObject m_TempSpawned;

    public override void Initialize()
    {
        base.Initialize();
        m_poolDictionary = new Dictionary<string, Queue<IPooledObject>>();

        for (int i = 0; i < m_Pools.Count; i++)
        {
            Queue<IPooledObject> activeOnPool = new Queue<IPooledObject>();
            Queue<IPooledObject> objectsOnPool = new Queue<IPooledObject>();

            for (int j = 0; j < m_Pools[i].SpawnedSize; j++)
            {
                m_TempSpawned = Instantiate(m_Pools[i].PooledPrefab, m_Pools[i].PrefabParent).GetComponent<IPooledObject>();
                m_TempSpawned.GetGameObject().gameObject.SetActive(false);
                m_TempSpawned.GetGameObject().Initialize();
                m_TempSpawned.SetPooledTag(m_Pools[i].PrefabTag);
                objectsOnPool.Enqueue(m_TempSpawned);
            }
            m_poolDictionary.Add(m_Pools[i].PrefabTag, objectsOnPool);

        }
    }
    public IPooledObject SpawnFromPool(string _prefabTag,
                                    Vector3 _position = new Vector3(),
                                    Quaternion _rotation = new Quaternion(),
                                    Transform _parent = null)
    {
        if (!m_poolDictionary.ContainsKey(_prefabTag))
        {
            return null;
        }

        if (m_poolDictionary[_prefabTag].Count > 0)
        {
            m_SpawnOnPool = m_poolDictionary[_prefabTag].Dequeue();
        }
        else
        {
            for (int i = m_Pools.Count - 1; i >= 0; i--)
            {
                if (m_Pools[i].PrefabTag == _prefabTag)
                {
                    m_SpawnOnPool = Instantiate(m_Pools[i].PooledPrefab, m_Pools[i].PrefabParent).GetComponent<IPooledObject>();
                    m_SpawnOnPool.GetGameObject().Initialize();
                    m_SpawnOnPool.SetPooledTag(m_Pools[i].PrefabTag);
                    break;
                }
            }
        }



        if (_position != null)
        {
            m_SpawnOnPool.GetGameObject().transform.position = _position;
        }
        if (_rotation != null)
        {
            m_SpawnOnPool.GetGameObject().transform.rotation = _rotation;
        }

        m_SpawnOnPool.GetGameObject().transform.SetParent(_parent);

        m_SpawnOnPool.GetGameObject().gameObject.SetActive(true);
        m_SpawnOnPool.OnObjectSpawn();

        return m_SpawnOnPool;
    }

    public void AddObjectPool(string _prefabTag, IPooledObject _pooledObject)
    {
        if (!m_poolDictionary[_prefabTag].Contains(_pooledObject))
            m_poolDictionary[_prefabTag].Enqueue(_pooledObject);
    }
}