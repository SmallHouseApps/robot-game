using UnityEngine;
using System.Collections;

//using GooglePlayGames;               
//using UnityEngine.SocialPlatforms;
/// <summary>
/// ***********Uncomment lines after importing google play services*******************
/// </summary>

public class GooglePlayManager : MonoBehaviour
{

    public static GooglePlayManager singleton;

    //private const string LEADERBOARDS_SCORE = "CgkI4MTMtpUQEAIQAA"; //replace this id with yours

    private AudioSource sound;

    [HideInInspector]
    public ManageVariables vars;

    void OnEnable()
    {
        vars = Resources.Load("ManageVariablesContainer") as ManageVariables;
    }

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        //    PlayGamesPlatform.Activate();
        //    Social.localUser.Authenticate((bool success) =>
        //    {
        //        if (success)
        //        {
        //            
        //        }
        //    });

    }

    void OnLevelWasLoaded()
    {
        ReportScore(GameManager.instance.currentScore);
    }

    public void OpenLeaderboardsScore()
    {
        //if (Social.localUser.authenticated)
        //{
        //    PlayGamesPlatform.Instance.ShowLeaderboardUI(vars.leaderBoardID);
        //}
    }

    void ReportScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, vars.leaderBoardID, (bool success) => { });
        }
    }

}
