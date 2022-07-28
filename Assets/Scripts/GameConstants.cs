using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public static int PlayerLives = 3;
    public static int GreenChilliCount = 1;
    public static int RedChilliCount = -100;
    public static int BlueChilliCount = 1;
    public static int GoldenChilliCount = 10;
    public static string CharacterType = "boy";
    public static string GameType = "Single";
    public static int SceneLoaded = 0;
    public static string JoinedTournamentId;
    public static bool IsPaused = false;
    public static CharacterData SelectedPlayerForGame;
    public static int SelectedPowerupNumber;
    public static float GyroSetValue = 120;
}
