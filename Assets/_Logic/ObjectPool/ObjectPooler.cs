using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int poolSize;
    }

    public static ObjectPooler Instance {  get; private set; }

    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        poolDic = new();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objPool = new();

            for(int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDic.Add(pool.key, objPool);
        }
    }

    public GameObject GetFromPool(string key, Vector3 pos, Quaternion rot)
    {
        if(!poolDic.ContainsKey(key))
        {
            Debug.LogWarning($"Pool with key: {key} does not exist!");
            return null;
        }

        GameObject spawnObj = poolDic[key].Dequeue();

        spawnObj.SetActive(true);
        spawnObj.transform.position = pos;
        spawnObj.transform.rotation = rot;

        poolDic[key].Enqueue(spawnObj);

        return spawnObj;
    }
}
