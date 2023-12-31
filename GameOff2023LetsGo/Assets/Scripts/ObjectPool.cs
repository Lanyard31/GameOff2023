using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 20;

    private List<GameObject> pool;

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            //parented here so that the hierarchy doesn't get cluttered
            GameObject obj = Instantiate(prefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }

        GameObject newObj = Instantiate(prefab);
        newObj.transform.parent = transform;
        newObj.SetActive(false);
        pool.Add(newObj);

        return newObj;
    }
}
