using UnityEngine;
using System.Collections;
//using UnityEngine.Advertisements;

namespace MadFireOn
{

    public class UnityAds : MonoBehaviour
    {

        public static UnityAds instance;

        private int i = 0;

        [HideInInspector]
        public ManageVariables vars;

        void OnEnable()
        {
            vars = Resources.Load("ManageVariablesContainer") as ManageVariables;
        }

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            i = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance == null)
                return;

            if (GameManager.instance.gameOver == true)
            {
                //we want only one ad to be shown so we put condition that when i is 0 we show ad.
                if (i == 0)
                {
                    i++;
                    GameManager.instance.gamesPlayed++;

                    if (GameManager.instance.gamesPlayed >= vars.showInterstitialAfter)
                    {
                        GameManager.instance.gamesPlayed = 0;
                        //use any one of them
                        //admob ads
                        //AdsManager.instance.ShowInterstitial();
                        //unity ads
                        //ShowAd();
                    }
                }
            }
        }

        public void ShowAd()
        {
            //if (Advertisement.IsReady())
            //{
            //    Advertisement.Show();
            //}
        }

        //use this function for showing reward ads
        public void ShowRewardedAd()
        {
            //uncomment after importing unity ads
            //if (Advertisement.IsReady("rewardedVideo"))
            //{
            //    var options = new ShowOptions { resultCallback = HandleShowResult };
            //    Advertisement.Show("rewardedVideo", options);
            //}
            //else
            //{
            //    Debug.Log("Ads not ready");

            //}
        }

        //private void HandleShowResult(ShowResult result)
        //{
        //    switch (result)
        //    {
        //        case ShowResult.Finished:
        //            Debug.Log("The ad was successfully shown.");

        /*here we give 50 poinst as reward*/
        //            GameManager.instance.points += 5;
        //            GameManager.instance.Save();

        //            break;
        //        case ShowResult.Skipped:
        //            Debug.Log("The ad was skipped before reaching the end.");
        //            break;
        //        case ShowResult.Failed:
        //            Debug.LogError("The ad failed to be shown.");

        //            break;
        //    }
        //}

    }
}//namespace
