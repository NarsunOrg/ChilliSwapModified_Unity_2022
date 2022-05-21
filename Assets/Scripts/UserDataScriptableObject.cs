using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UserDataScripableObject", order = 1)]
public class UserDataScriptableObject : ScriptableObject
{
    [SerializeField]
    public PlayerAvatars MyAvatars;
}
[Serializable]
public class PlayerAvatarData
{
    public List<bool> ActiveObjects;
    public string PlayerType;
}
[Serializable]
public class PlayerAvatars
{
    public List<PlayerAvatarData> AllAvatars;
}
