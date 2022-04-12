using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _Player;
    public Transform[] _playerSpawnPoints;
    private int _PlayerRandomSpawnPos;
    
    void Start()
    {
        _PlayerRandomSpawnPos = Random.Range(0, _playerSpawnPoints.Length);
        _Player.transform.position = _playerSpawnPoints[_PlayerRandomSpawnPos].position;
        _Player.transform.rotation = _playerSpawnPoints[_PlayerRandomSpawnPos].rotation;
    }

   
}
