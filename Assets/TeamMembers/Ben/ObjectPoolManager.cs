using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PoolType
{
    PlayerBullet,
    None
}

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolParent;

    private static GameObject _playerBulletPoolParent;

    private void Awake() {
        SetupPoolParentObjects();
    }

    private void SetupPoolParentObjects()
    {
        _objectPoolParent = new GameObject("Pooled Objects");

        _playerBulletPoolParent = new GameObject("Player Bullets");
        _playerBulletPoolParent.transform.SetParent(_objectPoolParent.transform);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, PoolType poolType = PoolType.None)
    {
        return SpawnObject(objectToSpawn, Vector3.zero, Quaternion.identity, poolType);
    }
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == objectToSpawn.name);

        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        // Check for inactive objects in the pool
        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if(spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if(parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // Remove the "(Clone)" from the name

        PooledObjectInfo pool = ObjectPools.Find(x => x.LookupString == goName);

        if(pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch(poolType)
        {
            case PoolType.PlayerBullet:
                return _playerBulletPoolParent;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}

public class PooledObjectInfo // Think of this as one pool of objects
{ 
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
