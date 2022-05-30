using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

//GetProfile API STRUCTURE
[Serializable]
public class GetProfileAPIResponse
{
    public string[] nftTokenIds;
    public int ChilliTokenAmount;
    public float coversionRate;
    public string minChilliConvert;
    public int CollectedChillis;
    public ConfiguredCharactersData ConfiguredCharacters;
}
[Serializable]
public class ConfiguredCharactersData
{
    public string _id;
    public string userAddress;
    public string userId;
    public GetProfileCharactersData[] characterData;
}
[Serializable]
public class GetProfileCharactersData
{
    public string _id;
    public string skintone;
    public string hairstyle;
    public string headwear;
    public string eyecolor;
    public string[] clothes;
    public string goggles;
    public string headphones;
    public string shoes;
    public string bodytype;
    public string backpack;
    public string watch;
}

//GetProfile API STRUCTURE END

//GetAllTournaments API STRUCTURE
[Serializable]
public class GetAllTournamnetsAPIResponse
{
    public string currentDate;
    public AllTournamentsData[] data;
}

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
//GetAllTournaments API STRUCTURE END


//PostTournamentResult API STRUCTURE
[Serializable]
public class TournamentResultData
{
    public string tournament_id;
    public string distance;
    public string time;
    public string collected_chillies;
}
//PostTournamentResult API STRUCTURE END


//GetLeaderboard API STRUCTURE
public class GetLeaderboardAPIResponse
{
    //public LeaderboardData[] ;
}

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


//SetCharacter API STRUCTURE
[Serializable]
public class SetCharacterData
{
    public string skintone = "";
    public string hairstyle = "";
    public string eyecolor = "";
    public string[] clothes;
    public string[] accessories;
}
//SetCharacter API STRUCTURE END

public class APIManager : MonoBehaviour
{
    public static APIManager instance;


    private string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYyNmE1Mjk2ZmQ0Yzg1MTk5MzVhZGM2OCIsInB1YmxpY0FkZHJlc3MiOiIweDBjNzI5YzFmODFlOGE1Y2UxNzYwN2UwMDMzODFkOGQ0NDhlYTU1ZGYiLCJpYXQiOjE2NTExMzY4OTl9.Iw_n8y0TEkIDrHFhW3iJWwAmmOP5ohtzufRlodU-tX4";
    private string GetProfileURL = "http://54.179.83.173/api/users/getProfile";
    private string FindAllTournamentURL = "http://54.179.83.173/api/tournament";
    private string PostTournamentResultURL = "http://54.179.83.173/api/tournament/result";
    private string GetLeaderBoardURL = "http://54.179.83.173/api/leadboard/6269242b287e913281f770f3";
    private string SetCharacterURL = "http://54.179.83.173/api/character/set";
    private string GetSwapChilliesURL = "http://54.179.83.173/api/users/chilliToToken";

    public GetAllTournamnetsAPIResponse GetAllTournamnetsAPIResponseVar;
    public GetProfileAPIResponse GetProfileAPIResponseVar;
    public SetCharacterData[] SetCharacter;
    public GetLeaderboardAPIResponse GetLeaderboardAPIResponseVar;
    public LeaderboardData LeaderboardData;
    public GameObject TournamentButton;
    public Transform TournamentScrollContent;
    private TimeSpan diffStartTime;
    private TimeSpan diffEndTime;

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
        //SetCharacter = new SetCharacterData[2];
        GetProfileAPI();
        //GetAllTournamentsAPI();
        //PostTournamentResultApi();
        GetLeaderboardAPI();
        //Invoke("SetCharacterApi", 2f);
        //SetCharacterApi();
        //GetSwapChilliesApi();
        //
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
            GetProfileAPIResponseVar = new GetProfileAPIResponse();
            GetProfileAPIResponseVar = JsonUtility.FromJson<GetProfileAPIResponse>(res.Text);

            UISelectionManager.instance.UserTokens.text = GetProfileAPIResponseVar.ChilliTokenAmount.ToString();
            UISelectionManager.instance.UserTotalCollectedChillies.text = GetProfileAPIResponseVar.CollectedChillis.ToString();

            Debug.Log(GetProfileAPIResponseVar.nftTokenIds[0]);
            Debug.Log(GetProfileAPIResponseVar.ChilliTokenAmount);
            Debug.Log(GetProfileAPIResponseVar.CollectedChillis);
            Debug.Log(GetProfileAPIResponseVar.coversionRate);
            Debug.Log(GetProfileAPIResponseVar.ConfiguredCharacters._id);
            Debug.Log(GetProfileAPIResponseVar.ConfiguredCharacters.userAddress);
            Debug.Log(GetProfileAPIResponseVar.ConfiguredCharacters.characterData[0]._id);

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
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
            },

        }).Then(res =>
        {
            Debug.Log("responce received of GetAllTournamentsAPI");
            Debug.Log(res.Text);
            GetAllTournamnetsAPIResponseVar = new GetAllTournamnetsAPIResponse();
            GetAllTournamnetsAPIResponseVar = JsonUtility.FromJson<GetAllTournamnetsAPIResponse>(res.Text);

            Debug.Log(GetAllTournamnetsAPIResponseVar.currentDate);
            string CurrentDateTime = GetAllTournamnetsAPIResponseVar.currentDate;
            DateTime currentTime = DateTime.Parse(CurrentDateTime).ToUniversalTime();
            print(currentTime.ToString("HH:mm:ss"));

            Debug.Log(GetAllTournamnetsAPIResponseVar.data[0]._id);
            Debug.Log(GetAllTournamnetsAPIResponseVar.data.Length);
            for (int i = 0; i < GetAllTournamnetsAPIResponseVar.data.Length; i++)
            {
                GameObject TournamentObj = Instantiate(TournamentButton, TournamentScrollContent);
                TournamentDetail _TournamentDetails = TournamentObj.GetComponent<TournamentDetail>();
                _TournamentDetails.TournamentName_Text.text = GetAllTournamnetsAPIResponseVar.data[i].tournament_name;
                _TournamentDetails.Tournamentid = GetAllTournamnetsAPIResponseVar.data[i]._id;
                //_TournamentDetails.TournamentDuration_Text.text = GetAllTournamnetsAPIResponseVar.data[i].duration;

                string StartDateTime = GetAllTournamnetsAPIResponseVar.data[i].start_date;
                DateTime startTime = DateTime.Parse(StartDateTime).ToUniversalTime();
                startTime.AddHours(1);
                Debug.Log("Start Time: " + startTime);

                string EndDateTime = GetAllTournamnetsAPIResponseVar.data[i].end_date;
                DateTime endTime = DateTime.Parse(EndDateTime).ToUniversalTime();
                endTime.AddHours(2);
                Debug.Log("End Time: " + endTime);

                diffStartTime = new TimeSpan(((currentTime.Hour - 24) * 1) + startTime.Hour , ((currentTime.Minute - 60) * 1) + startTime.Minute , ((currentTime.Second - 60) * 1) + startTime.Second);
                //diffStartTime = (startTime - currentTime);
                diffEndTime = endTime - currentTime;
              //  Debug.Log("" + (((currentTime.Hour - 24) * 1) + startTime.Hour + startTime.Minute + startTime.Second));
                Debug.Log("Diff Start time: " + diffStartTime);
                Debug.Log("Diff End Time: " + diffEndTime);

                if (diffStartTime > TimeSpan.Zero)
                {
                    Debug.Log("diffStartTime > TimeSpan.Zero");
                    TournamentObj.GetComponent<Button>().interactable = false;
                }
                else
                {
                    if (diffEndTime > TimeSpan.Zero)
                    {
                        Debug.Log("diffEndTime > TimeSpan.Zero");
                        TournamentObj.GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        Debug.Log("diffEndTime > TimeSpan.Zero elseeeeee");
                        TournamentObj.GetComponent<Button>().interactable = false;
                    }
                }
                Debug.Log("Diff Start time: " + diffStartTime + " " +"hours"+ " " + diffStartTime.Hours);
                _TournamentDetails.Timer = diffStartTime;
                
                //_TournamentDetails.Timer_Text.text = string.Format("{0:00}:{1:00}:{2}", diffStartTime.Hours, diffStartTime.Minutes, diffStartTime.Seconds.ToString().Substring(0, 2));

            }
            
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
            GetLeaderboardAPIResponseVar = new GetLeaderboardAPIResponse();
            GetLeaderboardAPIResponseVar = JsonUtility.FromJson<GetLeaderboardAPIResponse>(res.Text);

            
           

        }).Catch(err =>
        {
            Debug.Log(err);
        });
    }

    public void SetCharacterApi()
    {
        SetCharacter[0].skintone = "Coin";
        SetCharacter[0].hairstyle = "test data";
        SetCharacter[0].eyecolor = "test data";
        SetCharacter[0].clothes = new string[2];
        SetCharacter[0].clothes[0] = "Shirt";
        SetCharacter[0].accessories = new string[2];
        SetCharacter[0].accessories[0] = "Apple";
        string temp = JsonUtility.ToJson( SetCharacter, true );
        RestClient.Request(new RequestHelper
        {
            Uri = SetCharacterURL,
            Method = "POST",
            //FormData = TournamentResultData,
            BodyRaw = new System.Text.UTF8Encoding().GetBytes(("["+temp+"]")),
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
                
            },
            
        }).Then(res =>
        {
            Debug.Log("responce received of SetCharacterApi");
            Debug.Log(res.Text);

        }).Catch(err =>
        {
            Debug.Log(temp);
            Debug.Log("Error Response" + err);
        });
    }

    public void GetSwapChilliesApi()
    {
        RestClient.Request(new RequestHelper
        {
            Uri = GetSwapChilliesURL,
            Method = "POST",
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
            },
        }).Then(res =>
        {
            Debug.Log("responce received of GetSwapChilliesApi");
            Debug.Log(res.Text);

        }).Catch(err =>
        {
            Debug.Log("Error Response" + err);
        });
    }
}
