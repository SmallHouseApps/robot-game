using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    //ref to buttons 
	public Button soundBtn, facebookBtn, rateBtn, shopBtn, playBtn, leaderboardBtn, settingBtn;
    public GameObject gamePanel; //ref to the panel
	public GameObject shopPanel, settingPanel; //ref to the shop panel
    public Text bestScore, scoreText, bestText, coinText, microchipText; //ref to text elements in the game
    public UIObjects UIO; //ref to the UIObject
    private AudioSource sfxSound; //ref to audiosource component

    [HideInInspector]
    public ManageVariables vars;

    void OnEnable()
    {
        vars = Resources.Load("ManageVariablesContainer") as ManageVariables;
    }

    // Use this for initialization
    void Start ()
    {
        sfxSound = GetComponent<AudioSource>();
		sfxSound.loop = true;
		sfxSound.clip = vars.bgMusic;
		sfxSound.Play ();
        MedalColor();
        //assign the respective methods to the buttons
		settingBtn.GetComponent<Button>().onClick.AddListener(() => { SettingBtn(); });
    //    soundBtn.GetComponent<Button>().onClick.AddListener(() => { SoundBtn(); });    
        facebookBtn.GetComponent<Button>().onClick.AddListener(() => { FacebookBtn(); });   
		shopBtn.GetComponent<Button>().onClick.AddListener(() => { ShopBtn(); });   
        playBtn.GetComponent<Button>().onClick.AddListener(() => { PlayBtn(); });    
        leaderboardBtn.GetComponent<Button>().onClick.AddListener(() => { LeaderboardBtn(); });
        rateBtn.GetComponent<Button>().onClick.AddListener(() => { RateBtn(); });

        //sound button
        if (GameManager.instance.isMusicOn == true)
        {
            AudioListener.volume = 1;
            UIO.soundIcon.sprite = vars.soundOffIcon;
        }
        else
        {
            AudioListener.volume = 0;
            UIO.soundIcon.sprite = vars.soundOnIcon;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {   //check if game has started
        if (GameManager.instance.startGame == true)
        {   //set the score text to current score value
            scoreText.text = "" + GameManager.instance.currentScore;
            //chekc if best score is defeated
            if (GameManager.instance.currentScore > GameManager.instance.bestScore)
            {   //if yes we update the best score and save it
                GameManager.instance.bestScore = GameManager.instance.currentScore;
                GameManager.instance.Save();
            }
        }
        //check if game is over
        if (GameManager.instance.gameOver == true)
            StartCoroutine(GameOver());//perform the specific code

        //check if game is not started and its a restart
        if (GameManager.instance.startGame == false && GameManager.instance.gameRestarted == true)
        {   //if yes then we change the bestText to score
            bestText.text = "Score";
            bestScore.text = "" + GameManager.instance.currentScore;//assign it the current score
            MedalColor();//perform medal method
        }
        //check if game is not started and its not a restart
        else if (GameManager.instance.startGame == false && GameManager.instance.gameRestarted == false)
        {
            bestText.text = "Best";
            bestScore.text = "" + GameManager.instance.bestScore;
        }

		coinText.text = "" + GameManager.instance.coins;
		microchipText.text = "" + GameManager.instance.microchips;
    }
    //Code perform after gameover
    IEnumerator GameOver()
    {   //wait for 0.5 sec
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.gameRestarted = true; //make game restarted true
        GameManager.instance.startGame = false;    //make game startgame false
        GameManager.instance.gameOver = false;     //make game over false
        GameManager.instance.scoreEffect = 0;      //reset the scoreeffect value to zero
        string sceneName = SceneManager.GetActiveScene().name; //get the scene name
        SceneManager.LoadScene(sceneName); //and loads it
    }

    /// <summary>
    /// Sound Button Method
    /// </summary>
    void SoundBtn()
    {   //depecnding on the value of isMusicOn the button sprite and game sound is set
        sfxSound.PlayOneShot(vars.buttonSound);
        if (GameManager.instance.isMusicOn == true)
        {
            GameManager.instance.isMusicOn = false;
            AudioListener.volume = 0;
            UIO.soundIcon.sprite = vars.soundOnIcon;
            GameManager.instance.Save();
        }
        else
        {
            GameManager.instance.isMusicOn = true;
            AudioListener.volume = 1;
            UIO.soundIcon.sprite = vars.soundOffIcon;
            GameManager.instance.Save();
        }
    }
	/// <summary>
	/// Facebook Button Method
	/// </summary>
	void SettingBtn()
	{
		sfxSound.PlayOneShot(vars.buttonSound);
		// settingPanel.SetActive(true);
	}


    /// <summary>
    /// Facebook Button Method
    /// </summary>
    void FacebookBtn()
    {
        sfxSound.PlayOneShot(vars.buttonSound);
        Application.OpenURL(vars.facebookBtnUrl);
    }

    /// <summary>
    /// Share Button Method
    /// </summary>
    void ShopBtn()
    {
        sfxSound.PlayOneShot(vars.buttonSound);
        // ShareScreenShot.instance.ButtonShare();
		shopPanel.SetActive(true);
    }

    /// <summary>
    /// Rate Button Method
    /// </summary>
    void RateBtn()
    {
        sfxSound.PlayOneShot(vars.buttonSound);
        Application.OpenURL(vars.rateButtonUrl);
    }

    /// <summary>
    /// Play Button Method
    /// </summary>
    void PlayBtn()
    {
        sfxSound.PlayOneShot(vars.buttonSound);
        GameManager.instance.currentScore = 0;
        gamePanel.SetActive(false);
        scoreText.text = "" + GameManager.instance.currentScore;
        scoreText.gameObject.SetActive(true);
        GameManager.instance.startGame = true;
    }

    /// <summary>
    /// Leaderboard Button Method
    /// </summary>
    void LeaderboardBtn()
    {
        sfxSound.PlayOneShot(vars.buttonSound);
#if UNITY_ANDROID
        GooglePlayManager.singleton.OpenLeaderboardsScore();
#elif UNITY_IOS
        LeaderboardiOSManager.instance.ShowLeaderboard();
#endif
    }

    /// <summary>
    /// Method which decide the medal to be shown depeneding on the score achieved
    /// </summary>
    void MedalColor()
    {
        if (GameManager.instance.currentScore >= 10)
        {
            UIO.medal.gameObject.SetActive(true);
            UIO.medal.sprite = vars.bronzeMedal;
        }
        else if (GameManager.instance.currentScore >= 25)
        {
            UIO.medal.gameObject.SetActive(true);
            UIO.medal.sprite = vars.silverMedal;
        }
        else if (GameManager.instance.currentScore >= 40)
        {
            UIO.medal.gameObject.SetActive(true);
            UIO.medal.sprite = vars.GoldMedal;
        }
        else if (GameManager.instance.currentScore < 10)
        {
            UIO.medal.gameObject.SetActive(false);
        }
    }

}
