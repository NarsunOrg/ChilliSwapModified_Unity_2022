using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] ObjectsSpawnPoints;
    public GameObject[] ObjectsSpawned;
    public bool IsObjectSpawned = false;

    void Start()
    {
       // Invoke("SpawnObjects", 3f);
        //Invoke("DestroyObjects", 5f);
    }

    public void SpawnObjects()
    {
        if (!IsObjectSpawned)
        {
            SpawnManager.instance.SpawnObjects(this, ObjectsSpawnPoints);
            IsObjectSpawned = true;
        }

    }
    public void SpawnNextPatchObjects()
    {
        for (int i = 0; i < SpawnManager.instance._ENV.Length; i++)
        {
            if (SpawnManager.instance._ENV[i] == this.transform.parent.gameObject)
            {
                if(i!= SpawnManager.instance._ENV.Length)
                SpawnManager.instance._ENV[i + 1].GetComponentInChildren<ObjectSpawner>().SpawnObjects();
               
            }
        }
    }

    public void DestroyObjects()
    {
        IsObjectSpawned = false;
        foreach (GameObject obj in ObjectsSpawned)
        {
            Destroy(obj);
        }
    }

    
}
