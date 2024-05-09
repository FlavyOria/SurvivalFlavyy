using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolXP : MonoBehaviour
{
    [SerializeField] GameObject objectToPool;
    [SerializeField] int poolCount = 100;

    List<GameObject> pooledObjects = new();

    private static ObjectPoolXP instance;

    public static ObjectPoolXP GetInstance() => instance;
    int poolIndex;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject g = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, transform);
            g.SetActive(false);
            pooledObjects.Add(g);
        }
    }

    public GameObject GetPooledObject()
    {
        poolIndex %= pooledObjects.Count; // Use pooledObjects.Count instead of poolCount
        GameObject p = pooledObjects[poolIndex++];
        p.GetComponent<IPoolable>().Reset();
        return p;
    }

}