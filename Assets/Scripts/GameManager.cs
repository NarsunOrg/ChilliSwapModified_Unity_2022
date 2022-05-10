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
    public GameObject ENV;

    private void Awake()
    {
        instance = this;
        if(GameConstants.CharacterType == "Boy")
        {
            _Player.transform.GetChild(0).gameObject.GetComponent<PlayerController>().SettingBoy();
        }
        else if (GameConstants.CharacterType == "Girl")
        {
            _Player.transform.GetChild(0).gameObject.GetComponent<PlayerController>().SettingGirl();
        }
        CurrentLives = GameConstants.PlayerLives;
        ENV.SetActive(true);
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
