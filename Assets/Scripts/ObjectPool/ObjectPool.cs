using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objectToPool;
    public int startSize;

    [SerializeField] private List<PooledObject> objectPool = new List<PooledObject>();
    [SerializeField] private List<PooledObject> usedPool = new List<PooledObject>();

    private PooledObject tempObj;
    // Start is called before the first frame update
    void Start()
    {
        IntializePool();
    }

    void IntializePool()

    {
        for (int i = 0; i < startSize; i++)
        {
            AddNewObject();
        }
    }

    void AddNewObject()
    {
        tempObj = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        tempObj.gameObject.SetActive(false);
        tempObj.SetObjectPool(this);
        objectPool.Add(tempObj);
    }

    public PooledObject GetPooledObject()
    {
        PooledObject tempObject;
        if (objectPool.Count > 0)
        {
            tempObject = objectPool[0];
            usedPool.Add(tempObject);
            objectPool.RemoveAt(0);
        }
        else
        {
            AddNewObject();
            tempObject = GetPooledObject();
        }

        tempObject.gameObject.SetActive(true);
        return tempObject;
    }

    public void DestroyPooledObject(PooledObject obj, float time = 0)
    {
        if( time == 0)
        {
            obj.Destroy();
        }
        else
        {
            obj.Destroy(time);
        }
    }

    public void RestoreObject(PooledObject obj)
    {
        //Debug.Log("Restored!");
        obj.gameObject.SetActive(false);
        usedPool.Remove(obj);
        objectPool.Add(obj);
    }
}
