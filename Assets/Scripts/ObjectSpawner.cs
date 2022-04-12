using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] ObjectsSpawnPoints;
    public GameObject[] ObjectsSpawned;

    void Start()
    {
        Invoke("SpawnObjects", 3f);
        //Invoke("DestroyObjects", 5f);
    }

    public void SpawnObjects()
    {
        SpawnManager.instance.SpawnObjects(this,ObjectsSpawnPoints);
    }

    public void DestroyObjects()
    {
        foreach (GameObject obj in ObjectsSpawned)
        {
            Destroy(obj);
        }
    }

    
}
