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
    public GameObject GreenChilliPrefab;
    public GameObject RedChilliPrefab;
    public GameObject GoldenChilliPrefab;
    public GameObject BlueChilliPrefab;
    public GameObject[] HurdlesPrefabs;
    public GameObject InvisibilityPrefab;
    public int PatternIndex = 0;
    int randomhurdle;
    private GameObject spawnObj;
    private int spawnHurdle = -1;
    public GameObject[] _ENV;
    public ObjectSpawner os;

    void Start()
    {
        instance = this;
    }

    public void SpawnObjects( ObjectSpawner OS, Transform[] t)
    {
        os = OS;
        GameObject[] ObjectSpwaned = new GameObject[t.Length];

        for (int i = 0; i < t.Length; i++)
        {
           
            switch (SpawnObjectsPatterns[Random.Range(0, SpawnObjectsPatterns.Length)].SpawnObjectsPattern[PatternIndex])
            {
                case "green":
                    spawnObj = Instantiate(GreenChilliPrefab, new Vector3(t[i].transform.position.x, 1.6f, t[i].transform.position.z), t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, spawnObj.transform.localPosition.z + (Random.Range(-1, 2) * 1f));
                    break;
                case "red":
                    spawnObj = Instantiate(RedChilliPrefab, new Vector3(t[i].transform.position.x, 1.6f, t[i].transform.position.z), t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, spawnObj.transform.localPosition.z + (Random.Range(-1, 2) * 1f));
                    break;
                case "golden":
                    spawnObj = Instantiate(GoldenChilliPrefab, new Vector3(t[i].transform.position.x, 1.6f, t[i].transform.position.z), t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, spawnObj.transform.localPosition.z + (Random.Range(-1, 2) * 1f));
                    break;
                case "blue":
                    spawnObj = Instantiate(BlueChilliPrefab, new Vector3(t[i].transform.position.x, 1.6f, t[i].transform.position.z), t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, spawnObj.transform.localPosition.z + (Random.Range(-1, 2) * 1f));
                    break;
                case "hurdle":
                    randomhurdle = Random.Range(0, HurdlesPrefabs.Length);
                    while (randomhurdle == spawnHurdle)
                    {
                        randomhurdle = Random.Range(0, HurdlesPrefabs.Length);
                    }
                    spawnObj = Instantiate(HurdlesPrefabs[randomhurdle], t[i].position, t[i].rotation);
                    spawnHurdle = randomhurdle;
                    
                    break;
                case "invisibility":
                    spawnObj = Instantiate(InvisibilityPrefab, new Vector3(t[i].transform.position.x, 1.6f, t[i].transform.position.z), t[i].rotation);
                    spawnObj.transform.localPosition = new Vector3(spawnObj.transform.localPosition.x, spawnObj.transform.localPosition.y, spawnObj.transform.localPosition.z + (Random.Range(-1, 2) * 1f));
                    break;

            }
            ObjectSpwaned[i] = spawnObj;
            PatternIndex++;
            if (PatternIndex == SpawnObjectsPatterns[Random.Range(0, SpawnObjectsPatterns.Length)].SpawnObjectsPattern.Length)
            {
                PatternIndex = 0;
            }

        }

        OS.ObjectsSpawned = ObjectSpwaned;
    }
   
}
