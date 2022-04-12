using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Patterns
{
    public string[] SpawnObjectsPattern;
   
}

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public Patterns[] SpawnObjectsPatterns;
    public string[] SpawnObjectsPattern;
    //public string[] SpawnObjectsPatternTwo;
    //public string[] SpawnObjectsPatternThree;
    public GameObject GreenChilliPrefab;
    public GameObject RedChilliPrefab;
    public GameObject GoldenChilliPrefab;
    public GameObject BlueChilliPrefab;
    public GameObject[] HurdlesPrefabs;
    public int PatternIndex = 0;
    private GameObject spawnObj;
    public GameObject[] _ENV;
    void Start()
    {
        instance = this;
      
    }

    public void SpawnObjects( ObjectSpawner OS, Transform[] t)
    {
        GameObject[] ObjectSpwaned = new GameObject[t.Length];

        for (int i = 0; i < t.Length; i++)
        {
            //GameObject obj = Instantiate(ObjectsToSpawn[RandomObj],t[i].position, t[i].rotation);
            //obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y, Random.Range(-1, 1) * 5);
            //ObjectSpwaned[i] = obj;
            switch (SpawnObjectsPatterns[Random.Range(0, SpawnObjectsPatterns.Length)].SpawnObjectsPattern[PatternIndex])
            {
                case "green":
                    spawnObj = Instantiate(GreenChilliPrefab, t[i].position, t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, Random.Range(-1, 1) * 5);
                    break;
                case "red":
                    spawnObj = Instantiate(RedChilliPrefab, t[i]. position, t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, Random.Range(-1, 1) * 5);
                    break;
                case "golden":
                    spawnObj = Instantiate(GoldenChilliPrefab, t[i].position, t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, Random.Range(-1, 1) * 5);
                    break;
                case "blue":
                    spawnObj = Instantiate(BlueChilliPrefab, t[i].position, t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, Random.Range(-1, 1) * 5);
                    break;
                case "hurdle":
                    spawnObj = Instantiate(HurdlesPrefabs[Random.Range(0, HurdlesPrefabs.Length)], t[i].position, t[i].rotation);
                    break;
               
            }
            ObjectSpwaned[i] = spawnObj;
            PatternIndex++;
            if (PatternIndex == SpawnObjectsPattern.Length)
            {
                PatternIndex = 0;
            }

        }

        OS.ObjectsSpawned = ObjectSpwaned;
    }
   
}
