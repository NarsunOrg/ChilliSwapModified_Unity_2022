using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using UnityEngine.Networking;

//GetProfile API STRUCTURE
[Serializable]
public class GetProfileData
{
    public int[] tokenIds;
    public int ChilliTokenAmount;
    public string UserName;
    public int CollectedChillis;
    //public string ConfiguredCharacters;
    
}
//GetProfile API STRUCTURE END

//GetAllTournamnets API STRUCTURE
[Serializable]
public class AllTournamentsData
{
    public bool active;
    public string _id;
    public string start_date;
    public string end_date;
    public string tournament_name;
    public string duration;
    public string timestamp;
}
//GetAllTournamnets API STRUCTURE END


//PostTournamnetResult API STRUCTURE
[Serializable]
public class TournamentResultData
{
    public string tournament_id;
    public string distance;
    public string time;
    public string collected_chillies;
   
}
//PostTournamnetResult API STRUCTURE END


//GetLeaderboard API STRUCTURE
[Serializable]
public class LeaderboardData
{
    public string _id;
    public string user;
    public string distance;
    public string time;
    public string collected_chillies;
    
}
//GetLeaderboard API STRUCTURE END


public class APIManager : MonoBehaviour
{
    public static APIManager instance;


    private string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYyNmE1Mjk2ZmQ0Yzg1MTk5MzVhZGM2OCIsInB1YmxpY0FkZHJlc3MiOiIweDBjNzI5YzFmODFlOGE1Y2UxNzYwN2UwMDMzODFkOGQ0NDhlYTU1ZGYiLCJpYXQiOjE2NTExMzY4OTl9.Iw_n8y0TEkIDrHFhW3iJWwAmmOP5ohtzufRlodU-tX4";
    private string GetProfileURL = "http://54.179.83.173/api/users/getProfile";
    private string FindAllTournamentURL = "http://54.179.83.173/api/tournament";
    private string PostTournamentResultURL = "http://54.179.83.173/api/tournament/result";
    private string GetLeaderBoardURL = "http://54.179.83.173/api/leadboard/6269242b287e913281f770f3";

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        GetProfileAPI();
        GetAllTournamentsAPI();
        PostTournamentResultApi();
        GetLeaderboardAPI();
    }

    public void GetProfileAPI()
    {
        RestClient.Request(new RequestHelper
        {
            Uri = GetProfileURL,
            Method = "GET",
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
            },

        }).Then(res =>
        {
            Debug.Log("responce received of GetProfileAPI");
            Debug.Log(res.Text);

        }).Catch(err =>
        {
            Debug.Log(err);
        });
    }

    public void GetAllTournamentsAPI()
    {
        RestClient.Request(new RequestHelper
        {

            Uri = FindAllTournamentURL,
            Method = "GET",
            //CertificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey(),
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
            },

        }).Then(res =>
        {
            Debug.Log("responce received of GetAllTournamentsAPI");
            Debug.Log(res.Text);



        }).Catch(err =>
        {
            Debug.Log(err);
        });


    }

    public void PostTournamentResultApi()
    {
        TournamentResultData _TournamentResultData = new TournamentResultData() ;
        _TournamentResultData.tournament_id = "6269242b287e913281f770f3";
        _TournamentResultData.distance = "2";
        _TournamentResultData.time = "9 am";
        _TournamentResultData.collected_chillies = "test";
       
        RestClient.Request(new RequestHelper
        {
            
            Uri = PostTournamentResultURL,
            Method = "POST",
            //FormData = TournamentResultData,
            BodyRaw = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(_TournamentResultData)),
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
                     },
        }).Then(res =>
        {
            Debug.Log("responce received of PostTournamentResultApi");
            Debug.Log(res.Text);

        }).Catch(err =>
        {
            Debug.Log("Error Response" + err);
        });
    }

    public void GetLeaderboardAPI()
    {
        RestClient.Request(new RequestHelper
        {
            Uri = GetLeaderBoardURL,
            Method = "GET",
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
            },

        }).Then(res =>
        {
            Debug.Log("responce received of GetLeaderboardAPI");
            Debug.Log(res.Text);

        }).Catch(err =>
        {
            Debug.Log(err);
        });
    }
}
