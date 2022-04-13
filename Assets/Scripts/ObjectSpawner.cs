using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform[] ObjectsSpawnPoints;
    public GameObject[] ObjectsSpawned;

    void Start()
    {
       // Invoke("SpawnObjects", 3f);
        //Invoke("DestroyObjects", 5f);
    }

    public void SpawnObjects()
    {
        SpawnManager.instance.SpawnObjects(this,ObjectsSpawnPoints);
     
    }
    public void SpawnNextPatchObjects()
    {
      
        for (int i = 0; i < SpawnManager.instance._ENV.Length; i++)
        {
            Debug.Log("callingggg");
            if (SpawnManager.instance._ENV[i] == this.transform.parent.gameObject)
            {
                if(i!= SpawnManager.instance._ENV.Length)
                SpawnManager.instance._ENV[i + 1].GetComponentInChildren<ObjectSpawner>().SpawnObjects();
                Debug.Log("callingggg2222222");
            }


        }
    }

    public void DestroyObjects()
    {
        foreach (GameObject obj in ObjectsSpawned)
        {
            Destroy(obj);
        }
    }

    
}
