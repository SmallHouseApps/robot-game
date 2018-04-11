using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

using GL = UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

[System.Serializable]
public class GameDesign : EditorWindow
{
    //Editor (3 sections Audio , MainMenu , Admob)
    Texture2D book;
    Texture2D GDbanner;
    bool[] toggles;
    string[] buttons;
    private static Texture2D bgColor;
    public GUISkin editorSkin;
    Vector2 scrollPosition = new Vector2(0, 0);
    string[] bannerPositionTexts = new string[] { "Bottom", "Bottom Left", "Bottom Right", "Top", "Top Left", "Top Right" };
    public ManageVariables vars;
    public UIObjects UIO;

    [MenuItem("Tools/Game Design")]
    static void Initialize()
    {
        GameDesign window = EditorWindow.GetWindow<GameDesign>(true, "GAME DESIGN");
        window.maxSize = new Vector2(500f, 585f);
        window.minSize = window.maxSize;
    }

    void OnEnable()
    {
        toggles = new bool[] { false, false, false };
        buttons = new string[] { "Open", "Open", "Open" };
        vars = (ManageVariables)AssetDatabase.LoadAssetAtPath("Assets/Game/Resources/ManageVariablesContainer.asset", typeof(ManageVariables));
        book = Resources.Load("question", typeof(Texture2D)) as Texture2D;
        GDbanner = Resources.Load("GDbanner", typeof(Texture2D)) as Texture2D;
        try
        {
            UIO = Camera.main.GetComponent<UIObjects>();
        }
        catch (Exception e) { }

        try
        {
            liveUpdate();
        }
        catch (Exception e) { }

    }

    void OnGUI()
    {
        // Settings
        bgColor = (Texture2D)Resources.Load("editorBgColor");
        GUI.DrawTexture(new Rect(0, 0, maxSize.x, maxSize.y), bgColor, ScaleMode.StretchToFill);
        GUI.skin = editorSkin;
        GL.Label(GDbanner);
        scrollPosition = GL.BeginScrollView(scrollPosition);

        #region UI Options
        blockHeader("UI Options", "All UI options.", 0);
        if (toggles[0])
        {
            buttons[0] = "Close";
            BVS("GroupBox"); //0

            // Content Start
            GL.Label("UI Images", "centerBoldLabel");
            GL.Space(10);

            BV(); //1

            BH();
            vars.buttonGui = (Sprite) EGL.ObjectField("Button GUI", vars.buttonGui, typeof(Sprite), false);
            vars.leaderboardIcon = (Sprite) EGL.ObjectField("Leaderboard Icon", vars.leaderboardIcon, typeof(Sprite), false);
            EH();

            BH();
            vars.rateIcon = (Sprite) EGL.ObjectField("Rate Icon", vars.rateIcon, typeof(Sprite), false);
            vars.facebookIcon = (Sprite) EGL.ObjectField("Facebook Icon", vars.facebookIcon, typeof(Sprite), false);
            EH();

            BH();
            vars.soundOnIcon = (Sprite) EGL.ObjectField("Sound On Icon", vars.soundOnIcon, typeof(Sprite), false);
            vars.soundOffIcon = (Sprite) EGL.ObjectField("Sound Off Icon", vars.soundOffIcon, typeof(Sprite), false);
            EH();

            BH();
            vars.playButton = (Sprite)EGL.ObjectField("Play Button", vars.playButton, typeof(Sprite), false);
            vars.bronzeMedal = (Sprite)EGL.ObjectField("Bronze", vars.bronzeMedal, typeof(Sprite), false);
            EH();

            BH();         
            vars.silverMedal = (Sprite)EGL.ObjectField("Silver", vars.silverMedal, typeof(Sprite), false);
            vars.GoldMedal = (Sprite)EGL.ObjectField("Gold", vars.GoldMedal, typeof(Sprite), false);
            EH();

            EV(); //1

            separator();
            GL.Label("UI Texts", "centerBoldLabel");
            GL.Space(10);

            BVS("GroupBox"); //2
            GL.Label("Main Menu");
            vars.shareBtnText = EGL.TextField("ShareText", vars.shareBtnText);
            vars.shareTextColor = EGL.ColorField("ShareTextColor", vars.shareTextColor);
            vars.bestTextColor = EGL.ColorField("BestTextColor", vars.bestTextColor);
            vars.scoreTextColor = EGL.ColorField("ScoreTextColor", vars.scoreTextColor);
            vars.inGameScoreTextColor = EGL.ColorField("InGameScoreTextColor", vars.inGameScoreTextColor);
            vars.gameTitleText = EGL.TextField("GameTitleText", vars.gameTitleText);
            vars.gameTitleColor = EGL.ColorField("GameTitleColor", vars.gameTitleColor);
            EV(); //2

            separator();
            GL.Label("UI Fonts", "centerBoldLabel");
            GL.Space(10);
            vars.gameFont = EGL.ObjectField("Game Font", vars.gameFont, typeof(Font), false) as Font;

            // Content End
            EditorUtility.SetDirty(vars);
            EV(); //0
        }
        else buttons[0] = "Open";
        EV();
        #endregion

        #region Sound Options
        blockHeader("Sound Options", "Sound & Music options.", 1);
        if (toggles[1])
        {
            buttons[1] = "Close";
            BVS("GroupBox");
            // Content Start
            vars.buttonSound = EGL.ObjectField("Button Sound", vars.buttonSound, typeof(AudioClip), false) as AudioClip;
            vars.jumpSound = EGL.ObjectField("Jump Sound", vars.jumpSound, typeof(AudioClip), false) as AudioClip;
            vars.gameOverSound = EGL.ObjectField("GameOver Sound", vars.gameOverSound, typeof(AudioClip), false) as AudioClip;
            vars.hiScore = EGL.ObjectField("HiScore Sound", vars.hiScore, typeof(AudioClip), false) as AudioClip;
            // Content End
            EditorUtility.SetDirty(vars);
            EV();
        }
        else buttons[1] = "Open";
        EV();
        // End Block
        #endregion

        #region Other Options
        // Start Block
        blockHeader("Other Options", "AdMob, Google Play Services and etc. options.", 2);
        if (toggles[2])
        {
            buttons[2] = "Close";
            GL.BeginVertical("GroupBox");

            //Admob
            GL.Label("AdMob Options", EditorStyles.boldLabel);

            //Banner
            vars.adMobBannerID = EGL.TextField("AdMob Banner ID", vars.adMobBannerID);
            GL.BeginHorizontal();
            GL.Label("Banner Position");
            vars.bannerAdPoisiton = GL.SelectionGrid(vars.bannerAdPoisiton, bannerPositionTexts, 3, "Radio");
            GL.EndHorizontal();
            separator();

            //Interstitial
            vars.adMobInterstitialID = EGL.TextField("AdMob Interstitial ID", vars.adMobInterstitialID);
            GL.BeginHorizontal();
            GL.Label("Show Interstitial After Death Times");
            vars.showInterstitialAfter = EGL.IntSlider(vars.showInterstitialAfter, 1, 25);
            GL.EndHorizontal();
            separator();

            //Google Play Service
            GL.Label("Google Play Or Game Center", EditorStyles.boldLabel);
            vars.leaderBoardID = EGL.TextField("Leaderboard ID", vars.leaderBoardID);
            separator();

            GL.Label("Other Options", EditorStyles.boldLabel);
            //Rate Url
            GL.BeginHorizontal();
            GL.Label("Rate Button Url", GL.Width(100f));
            vars.rateButtonUrl = EGL.TextArea(vars.rateButtonUrl, GL.Height(50f));
            GL.EndHorizontal();
            GL.Space(15f);
            //Facebook Url
            GL.BeginHorizontal();
            GL.Label("FB Page Url", GL.Width(100f));
            vars.facebookBtnUrl = EGL.TextArea(vars.facebookBtnUrl, GL.Height(50f));
            GL.EndHorizontal();
            GL.Space(15f);
            separator();

            EditorUtility.SetDirty(vars);
            GL.EndVertical();
        }
        else buttons[2] = "Open";
        GL.EndVertical();
        // End Block
        #endregion
        GL.EndScrollView();
        EditorUtility.SetDirty(vars);
        try
        {
            liveUpdate();
        }
        catch (Exception e) { }

    }

    void liveUpdate()
    {
        #region Button Images
        //for button gui
        UIO.shareBtn.sprite = vars.buttonGui;
        UIO.rateBtn.sprite = vars.buttonGui;
        UIO.leaderboardBtn.sprite = vars.buttonGui;
        UIO.soundBtn.sprite = vars.buttonGui;
        UIO.facebookBtn.sprite = vars.buttonGui;
        //for button icons
        UIO.rateIcon.sprite = vars.rateIcon;
        UIO.soundIcon.sprite = vars.soundOnIcon;
        UIO.leaderboardIcon.sprite = vars.leaderboardIcon;
        UIO.facebookIcon.sprite = vars.facebookIcon;
        UIO.playIcon.sprite = vars.playButton;
        #endregion

        #region Button Text
        UIO.shareBtnText.text = vars.shareBtnText;
        UIO.shareBtnText.color = vars.shareTextColor;
        UIO.shareBtnText.font = vars.gameFont;

        UIO.gameTitleText.text = vars.gameTitleText;
        UIO.gameTitleText.color = vars.gameTitleColor;
        UIO.gameTitleText.font = vars.gameFont;

        UIO.bestText.color = vars.bestTextColor;
        UIO.bestText.font = vars.gameFont;

        UIO.BestScoreText.color = vars.scoreTextColor;
        UIO.BestScoreText.font = vars.gameFont;

        UIO.inGameScoreText.color = vars.inGameScoreTextColor;
        UIO.inGameScoreText.font = vars.gameFont;

        #endregion
    }

    void OnDestroy()
    {
        //EditorUtility.SetDirty(vars);
    }

    void blockHeader(string mainHeader, string secondHeader, int blockIdex)
    {
        BV();
        GL.Label(mainHeader, "TL Selection H2");
        BH();
        if (GL.Button(buttons[blockIdex], GL.Height(25f), GL.Width(50f))) toggles[blockIdex] = !toggles[blockIdex];
        BHS("HelpBox");
        GL.Label(secondHeader, "infoHelpBoxText");
        GL.Label(book, GL.Height(18f), GL.Width(20f));
        EH();
        EH();
        GL.Space(3);
    }

    void separator()
    {
        GL.Space(10f);
        GL.Label("", "separator", GL.Height(1f));
        GL.Space(10f);
    }

    void BH()
    {
        GL.BeginHorizontal();
    }

    void BHS(string style)
    {
        GL.BeginHorizontal(style);
    }

    void EH()
    {
        GL.EndHorizontal();
    }

    void BVS(string style)
    {
        GL.BeginVertical(style);
    }

    void BV()
    {
        GL.BeginVertical();
    }

    void EV()
    {
        GL.EndVertical();
    }

    //[MenuItem("Tools/ManageVariables")]
    //private static void ManageVariables()
    //{
    //    string path = EditorUtility.SaveFilePanelInProject("New Manage Variables", "ManageVariables"
    //            , "asset", "Define the name for the ManageVariables asset");
    //    if (path != "")
    //    {
    //        ManageVariables dataClass = CreateInstance<ManageVariables>();
    //        AssetDatabase.CreateAsset(dataClass, path);
    //        AssetDatabase.Refresh();
    //        AssetDatabase.SaveAssets();
    //    }
    //}
        	

}
