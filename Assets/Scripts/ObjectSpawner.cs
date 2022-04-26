using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] ChilliesSpawnPoints;
    public Transform[] HurdlesSpawnPoints;
    public GameObject[] ChilliesSpawned;
    public GameObject[] HurdlesSpawned;
    public bool IsObjectSpawned = false;

   
    public void SpawnObjects()
    {
        if (!IsObjectSpawned)
        {
            Debug.Log("call");
            SpawnManager.instance.SpawnObjects(this, ChilliesSpawnPoints, HurdlesSpawnPoints);
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

    public void DestroyChillies()
    {
        IsObjectSpawned = false;
        foreach (GameObject obj in ChilliesSpawned)
        {
            Destroy(obj);
        }
    }

    public void DestroyHurdles()
    {
        IsObjectSpawned = false;
        foreach (GameObject obj in HurdlesSpawned)
        {
            Destroy(obj);
        }
    }
}
