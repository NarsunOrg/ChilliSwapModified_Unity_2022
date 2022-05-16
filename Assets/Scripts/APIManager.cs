using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using UnityEngine.Networking;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
//GetAllTournamnets API STRUCTURE

class AcceptAllCertificatesSignedWithASpecificPublicKey : CertificateHandler
{
    // Encoded RSAPublicKey
    private static string PUB_KEY = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCzUIE/VNon3o3UmP+qBtXoPg8w4FgL7Xh3VvFCKQreAlWXpTjyvUnRfH4mDoyR7anG2LvKlPDdo50FjXzUvZqD3/iGByq3UgMTtJ3VRK0J0ps+6ZpbcwZCFRMwddO63ed5/qwPcQvwzCCOA1mUmoE5aYyDZVPlqJolljHL2d75wCAEVyiCDWvS/wwz4IGxHdHWDrw/XhpM99AFIVXmp4AgdmIJEQjLjTtAg/zBKM7R7FIstxmFVb303/nIAGoaeoKFjt5gr/0hxV1zMVlO95lQgUygOn9ldpvW2sJ6ikz8mksZDqP47k7T41wRk9fVPx2fboU6+fcJNEsYLKBFwYfNAgMBAAECggEAERsg+VWy6hLFHP7rhODaDym8cUE1pQ2mbFwS7+jSbJN0bt8IK7/7Bs0Fi8PfjRxLEfkjERRcVgiBtkHlIrZjwyjeoIqWfJ6KRJr6Y5oFl+ZLgRjv7MFwW2V/SyQyaqU4q96rs11pcjNG98VCCnci1MNI8E/+TO5kpilJ3tSN/O057RUY1A4i2YdlN/zO43bXW35/p2uRfEneWaPJgu+4Hck/nscadoM98SoUyuTvjleQadh6Yd+ZlD10QoCsP/0yfeoI6flNipoCN/QHt+rK94cxE40Ni2YiVULPCFqnlz4JRUO0Kw2XIW/ryXdpife+SZ51vZKSkQsn2xGA2gJxUQKBgQDy7ehYm+1NKxfcOeD5MPbkSk64EWOqvMc6X45AQcfkxW+7H2ygi92Wg3jY/W8BY9jjBgg5Ip/97W9nq4o6U6TDdeMNwwv3DbI3+ao6POWvpSsv9lRNQB9ef+q7oqK6ZhZh71XiBsmS2kjVhi7vqieN7wPmE51DfwvEkDC3AthqRwKBgQC89l7Ysj9Iw1PUcc7FQS/SGQXTIOg34Z5gw3XNI25N/Hbz5EocBXjBjGXMQxTui24PRxOYttDMJ+jFNvUCYWQj07QNYXCdQh4WKYHkw606piuqjiHFC3m6VZSfCUyhvhXTFI2fcAYuLL6COq3/NLP3zWBUTS8ppXC4EVieRD/zSwKBgQDMTwZQL6OeeRWyJANv0Jx0V4JpEEbwe6BbGa57oFdLsjlL+RvU0ozkX2ItrMfFNYJdPN+BnftNBnnhiMXSVDKIKQdytY3ElAJFTXa7UCgkVxdBWTyBU+KOCasTb67Icb91UmK6m9a/6VHEMvwamNJ3boOq5ugmshzljhdKc9wrVQKBgQCk3GZftRQVjaUj4q891eO87+vvCfTQXF3rmly6v0DLdYrqurAVxohWhQGDtrsabDd3yNFGGaoNlHw3I/2bOBFAWMHsMqkn6rmJKGmVh0spsjTCtwKrgZmQgn4KSvi63Lb51CLDid86hfsob73CvN6PQnXa1wRg12CCl0+ztWP+BQKBgA6DvarmtsXCAiz72wC/CxLph/1TSCLgWctjadM9Vg1Q6UIK25gZTkprBoECF0X6WsDRz7Z3VIP0wGFtzCFSZoZklufcHxngx6Ls5jpoiUsm+4g2PNfgs9jK+h4ozSd+Q39IlfwBX7q1qbbGYLDcVO8Y9KZRDoZqvr6Jh1vIZKIb";

    protected override bool ValidateCertificate(byte[] certificateData)
    {
        


        Debug.Log("asdasdsads");
        X509Certificate2 certificate = new X509Certificate2(certificateData);
        string pk = certificate.GetPublicKeyString();
        if (pk.Equals(PUB_KEY))
            return true;

        // Bad dog
        return false;


        
    }
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
//GetAllTournamnets API STRUCTURE END

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
    private string FindAllTournamentURL = "http://game-api.chilliswap.org/api/tournament";
    private string PostTournamentResultURL = "http://game-api.chilliswap.org/api/tournament/result";
    private string GetLeaderBoardURL = "http://game-api.chilliswap.org/api/leadboard/6269242b287e913281f770f3";

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

        //HttpClientHandler clientHandler = new HttpClientHandler();
        //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        //// Pass the handler to httpclient(from you are calling api)
        //HttpClient client = new HttpClient(clientHandler);

        StartCoroutine(LoadLoginInfromation());
        GetAllTournamentsAPI();
        PostTournamentResultApi();
        GetLeaderboardAPI();
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



    IEnumerator LoadLoginInfromation()
    {
       
        UnityWebRequest www = UnityWebRequest.Get(FindAllTournamentURL);
        www.SetRequestHeader("x-access-token", authToken);
        //www.certificateHandler = new AcceptAllCertificatesSignedWithASpecificPublicKey();
        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");


        yield return www.SendWebRequest();
        
        if (www.result==UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Data: " + www.downloadHandler.text);
        }
    }

    public void PostTournamentResultApi()
    {
        string tournament_id = "6269242b287e913281f770f3";
        string distance = "2";
        string time = "9 am";
        string collected_chillies = "test";

        WWWForm TournamentResultData = new WWWForm();
        TournamentResultData.AddField("tournament_id", tournament_id);
        TournamentResultData.AddField("distance", distance);
        TournamentResultData.AddField("time", time);
        TournamentResultData.AddField("collected_chillies", collected_chillies);

        RestClient.Request(new RequestHelper
        {

            Uri = PostTournamentResultURL,
            Method = "POST",
            FormData = TournamentResultData,
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
