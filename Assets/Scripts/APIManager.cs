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
[Serializable]
public class GetLeaderboardAPIResponse
{
    public LeaderboardData[] Data;
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
    public string[] clothes=  { "", "" };
    public string bodytype = "";
    public string skintone = "";
    public string hairstyle = "";
    public string eyecolor = "";
    public string goggles = "";
    public string headphones = "";
    public string backpack = "";
    public string watch = "";
    public string shoes = "";
    public string headwear = "";
}

[Serializable]
public class SetCharacterAPIData
{
    public SetCharacterData[] SetCharacter;
}
//SetCharacter API STRUCTURE END

//PostChillies API STRUCTURE
[Serializable]
public class ChilliesData
{
    public int chillis;
}

[Serializable]
public class GetEarnChilliesAPIResponse
{
    public int chillis;
}
//PostChillies API STRUCTURE END


public class APIManager : MonoBehaviour
{
    public static APIManager instance;


    private string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYyNmE1Mjk2ZmQ0Yzg1MTk5MzVhZGM2OCIsInB1YmxpY0FkZHJlc3MiOiIweDBjNzI5YzFmODFlOGE1Y2UxNzYwN2UwMDMzODFkOGQ0NDhlYTU1ZGYiLCJpYXQiOjE2NTExMzY4OTl9.Iw_n8y0TEkIDrHFhW3iJWwAmmOP5ohtzufRlodU-tX4";

    //For WEBGL
    //private string GetProfileURL = "https://game-api.chilliswap.org/api/users/getProfile";
    //private string FindAllTournamentURL = "https://game-api.chilliswap.org/api/tournament";
    //private string PostTournamentResultURL = "https://game-api.chilliswap.org/api/tournament/result";
    //private string GetLeaderBoardURL = "https://game-api.chilliswap.org/api/leadboard/";
    //private string SetCharacterURL = "https://game-api.chilliswap.org/api/character/set";
    //private string GetSwapChilliesURL = "https://game-api.chilliswap.org/api/users/chilliToToken";
    //private string PostChilliesURL = "https://game-api.chilliswap.org/api/users/earnChilli";

    //For EDITOR
    private string GetProfileURL = "http://54.179.83.173/api/users/getProfile";
    private string FindAllTournamentURL = "http://54.179.83.173/api/tournament";
    private string PostTournamentResultURL = "http://54.179.83.173/api/tournament/result";
    private string GetLeaderBoardURL = "http://54.179.83.173/api/leadboard/";
    private string SetCharacterURL = "http://54.179.83.173/api/character/set";
    private string GetSwapChilliesURL = "http://54.179.83.173/api/users/chilliToToken";
    private string PostChilliesURL = "http://54.179.83.173/api/users/earnChilli";

    public GetAllTournamnetsAPIResponse GetAllTournamnetsAPIResponseVar;
    public GetProfileAPIResponse GetProfileAPIResponseVar;
    public SetCharacterAPIData CharacterAPIData;
    public GetLeaderboardAPIResponse GetLeaderboardAPIResponseVar;
    public GetEarnChilliesAPIResponse GetEarnChilliesAPIResponseVar;
    public GameObject TournamentButton;
    public GameObject LeaderboardRow;
    private TimeSpan diffStartTime;
    private TimeSpan diffEndTime;

    public CharacterData CD;

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

            for (int p = 0; p < 6; p++)
            {
                // Debug.Log(p);
              //  CharacterData CD = new CharacterData();

                CD.bodytype = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].bodytype;
                CD.skintone = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].skintone;
                CD.hairstyle = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].hairstyle;
                CD.eyecolor = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].eyecolor;
                CD.goggles = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].goggles;
                CD.headphones = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].headphones;
                CD.backpack = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].backpack;
                CD.watch = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].watch;
                CD.shoes = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].shoes;
                CD.headwear = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].headwear;
                CD.clothes = GetProfileAPIResponseVar.ConfiguredCharacters.characterData[p].clothes;

                SlotManager.instance.CharacterList[p].GetComponent<CharacterCustomizer>().ChangeData(CD);
            }

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
                GameObject TournamentObj = Instantiate(TournamentButton, UISelectionManager.instance.TournamentScrollContent);
                TournamentDetail _TournamentDetails = TournamentObj.GetComponent<TournamentDetail>();
                _TournamentDetails.TournamentName_Text.text = GetAllTournamnetsAPIResponseVar.data[i].tournament_name;
                _TournamentDetails.Tournamentid = GetAllTournamnetsAPIResponseVar.data[i]._id;

                string StartDateTime = GetAllTournamnetsAPIResponseVar.data[i].start_date;
                DateTime startTime = DateTime.Parse(StartDateTime).ToUniversalTime();
               
                string EndDateTime = GetAllTournamnetsAPIResponseVar.data[i].end_date;
                DateTime endTime = DateTime.Parse(EndDateTime).ToUniversalTime();
               
                diffStartTime = new TimeSpan(startTime.Hour - currentTime.Hour, startTime.Minute - currentTime.Minute , startTime.Second - currentTime.Second);
                diffEndTime = endTime - currentTime;
              
                if (diffStartTime > TimeSpan.Zero)
                {
                    _TournamentDetails.Timer = diffStartTime;
                }
                else
                {
                    if (diffEndTime > TimeSpan.Zero)
                    {
                        _TournamentDetails.TimerHour_Text.text = "00";
                        _TournamentDetails.TimerMinutes_Text.text = "00";
                        _TournamentDetails.TimerSeconds_Text.text = "00";
                        _TournamentDetails.TimerImage.SetActive(false);
                        _TournamentDetails.JoinButton.SetActive(true);
                    }
                    else
                    {
                        _TournamentDetails.JoinButton.SetActive(false);
                    }
                }
               
            }
            
        }).Catch(err =>
        {
            Debug.Log(err);
        });
    }

    public void PostTournamentResultApi(string _TournamentId, string _Distance, string _Time, string _CollectedChillis)
    {
        TournamentResultData _TournamentResultData = new TournamentResultData() ;
        _TournamentResultData.tournament_id = _TournamentId;
        _TournamentResultData.distance = _Distance;
        _TournamentResultData.time = _Time;
        _TournamentResultData.collected_chillies = _CollectedChillis;
       
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

    public void GetLeaderboardAPI(Transform LeaderboardContent)
    {
        RestClient.Request(new RequestHelper
        {
            Uri = GetLeaderBoardURL + GameConstants.JoinedTournamentId, 
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

            for (int i = 0; i < GetLeaderboardAPIResponseVar.Data.Length; i++)
            {
                GameObject LeaderboardObj = Instantiate(LeaderboardRow, LeaderboardContent);
                LeaderboardDetails _LeaderboardDetails = LeaderboardObj.GetComponent<LeaderboardDetails>();
                _LeaderboardDetails.RankText.text = (i+1).ToString();
                _LeaderboardDetails.UserNameText.text = GetLeaderboardAPIResponseVar.Data[i].user;
                _LeaderboardDetails.TimeHoursText.text = (TimeSpan.FromSeconds(float.Parse(GetLeaderboardAPIResponseVar.Data[i].time)).Hours).ToString("00");
                _LeaderboardDetails.TimeMinutesText.text = (TimeSpan.FromSeconds(float.Parse(GetLeaderboardAPIResponseVar.Data[i].time)).Minutes).ToString("00");
                _LeaderboardDetails.TimeSecondsText.text = (TimeSpan.FromSeconds(float.Parse(GetLeaderboardAPIResponseVar.Data[i].time)).Seconds).ToString("00");
                _LeaderboardDetails.DistanceCoveredText.text = GetLeaderboardAPIResponseVar.Data[i].distance;
                _LeaderboardDetails.ChilliesCollectedText.text = GetLeaderboardAPIResponseVar.Data[i].collected_chillies;
            }

        }).Catch(err =>
        {
            Debug.Log(err);
        });
    }

    public void SetCharacterApi()
    {

        for (int p = 0; p < 6; p++)
        {

            CharacterData CD = SlotManager.instance.CharacterList[p].GetComponent<CharacterCustomizer>().MyData;

            CharacterAPIData.SetCharacter[p].clothes = CD.clothes;
            CharacterAPIData.SetCharacter[p].bodytype = CD.bodytype;
            CharacterAPIData.SetCharacter[p].skintone = CD.skintone;
            CharacterAPIData.SetCharacter[p].hairstyle = CD.hairstyle;
            CharacterAPIData.SetCharacter[p].eyecolor = CD.eyecolor;
            CharacterAPIData.SetCharacter[p].goggles = CD.goggles;
            CharacterAPIData.SetCharacter[p].headphones = CD.headphones;
            CharacterAPIData.SetCharacter[p].backpack = CD.backpack;
            CharacterAPIData.SetCharacter[p].watch = CD.watch;
            CharacterAPIData.SetCharacter[p].shoes = CD.shoes;
            CharacterAPIData.SetCharacter[p].headwear = CD.headwear;
        }
        
        string temp = "[" + JsonUtility.ToJson(CharacterAPIData) + "]";
        Debug.Log( temp );
        RestClient.Request(new RequestHelper
        {
            Uri = SetCharacterURL,
            Method = "POST",
            //FormData = TournamentResultData,
            BodyRaw = new System.Text.UTF8Encoding().GetBytes(temp),
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
            if (int.Parse(UISelectionManager.instance.UserTotalCollectedChillies.text) < 500)
            {
                UISelectionManager.instance.ProfileInnerPanel.SetActive(false);
                UISelectionManager.instance.SwapChilliMessagePanel.SetActive(true);
            }

        }).Catch(err =>
        {
            
            Debug.Log("Error Response" + err);
        });
    }

    public void PostChilliesApi(int ChilliesAmount)
    {
        ChilliesData _ChilliesData = new ChilliesData();
        _ChilliesData.chillis = ChilliesAmount;
        Debug.Log(JsonUtility.ToJson(_ChilliesData));
        RestClient.Request(new RequestHelper
        {
            Uri = PostChilliesURL,
            Method = "POST",
            //FormData = TournamentResultData,
            BodyRaw = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(_ChilliesData)),
            Headers = new Dictionary<string, string> {
                         { "x-access-token", authToken }
            },
        }).Then(res =>
        {
            Debug.Log("responce received of PostTournamentResultApi");
            Debug.Log(res.Text);
            GetEarnChilliesAPIResponseVar = new GetEarnChilliesAPIResponse();
            GetEarnChilliesAPIResponseVar = JsonUtility.FromJson<GetEarnChilliesAPIResponse>(res.Text);
            GetProfileAPIResponseVar.CollectedChillis = GetEarnChilliesAPIResponseVar.chillis;
        }).Catch(err =>
        {
            Debug.Log("Error Response" + err);
        });
    }
}
