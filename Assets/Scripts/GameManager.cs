using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject _Player;
    public Transform[] _playerSpawnPoints;
    private int _PlayerRandomSpawnPos;
    public int CurrentLives;
    public int CollectedChillis;

    private void Awake()
    {
        instance = this;
        CurrentLives = GameConstants.PlayerLives;
    }

    void Start()
    {
        _PlayerRandomSpawnPos = Random.Range(0, _playerSpawnPoints.Length);
        _Player.transform.position = _playerSpawnPoints[_PlayerRandomSpawnPos].position;
        _Player.transform.rotation = _playerSpawnPoints[_PlayerRandomSpawnPos].rotation;
    }

    public void OnRestartButton(int scenenumber)
    {
        SceneManager.LoadScene(scenenumber);
    }
}
