using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class ManageVariables : ScriptableObject {

    [SerializeField]
    public Sprite leaderboardIcon, soundOnIcon, soundOffIcon, facebookIcon, playButton, rateIcon, buttonGui;
    [SerializeField]
    public Sprite bronzeMedal, silverMedal, GoldMedal;
    [SerializeField]
    public Color32 gameTitleColor, shareTextColor, bestTextColor, inGameScoreTextColor, scoreTextColor;
    [SerializeField]
    public string gameTitleText, shareBtnText;
    [SerializeField]
    public Font gameFont;
    [SerializeField]
    public AudioClip buttonSound, jumpSound, gameOverSound, hiScore, bgMusic;

    // Standart Vars
    [SerializeField]
    public string adMobInterstitialID, adMobBannerID, rateButtonUrl, leaderBoardID, facebookBtnUrl;
    [SerializeField]
    public int showInterstitialAfter, bannerAdPoisiton;

}
