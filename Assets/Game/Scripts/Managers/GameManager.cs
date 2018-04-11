using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private GameData data;

    #region Variables not saved on device
    [HideInInspector]
    public bool gameOver = false, startGame = false, gameRestarted = false;
    [HideInInspector] // score effect to call effect only once in a game when new hi score is made
	public int currentScore, gamesPlayed = 0, scoreEffect = 0;
    #endregion

    #region Variables saved on device
    //variables which are saved on the device
    [HideInInspector]
    public bool fbBtnClicked, twitterBtnClicked, isMusicOn, isGameStartedFirstTime;
    [HideInInspector]
    public int bestScore, lastScore, coins, microchips;
    [HideInInspector]
    public bool[] achievements;

    #endregion

    void Awake()
    {
        MakeSingleton();
        InitializeGameVariables();
    }

    // Use this for initialization
    void Start()
    {
		  
	}

    void MakeSingleton()
    {
        //this state that if the gameobject to which this script is attached , if it is present in scene then destroy the new one , and if its not present
        //then create new 
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameVariables()
    {
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
			bestScore = lastScore = coins = microchips = 0;

            achievements = new bool[5];//if you want ot add more achievements change the value here
            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }

            fbBtnClicked = twitterBtnClicked = false;
                

            data = new GameData();

            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setMusicOn(isMusicOn);
            data.setFbClick(fbBtnClicked);
            data.setTwitterClick(twitterBtnClicked);
            data.setBestScore(bestScore);
            data.setLastScore(lastScore);
            data.setAchievementsUnlocked(achievements);
			data.setCoins(coins);
			data.setMicrochips(microchips);
            Save();

            Load();
        }
        else
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getMusicOn();
            fbBtnClicked = data.getFbClick();
            twitterBtnClicked = data.getTwitterClick();
            bestScore = data.getBestScore();
            lastScore = data.getLastScore();
			coins = data.getCoins();
			microchips = data.getMicrochips();
            achievements = data.getAchievementsUnlocked();
        }
    }


    //                              .........this function take care of all saving data like score , current player , current weapon , etc
    public void Save()
    {
        FileStream file = null;
        //whicle working with input and output we use try and catch
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setMusicOn(isMusicOn);
                data.setFbClick(fbBtnClicked);
                data.setTwitterClick(twitterBtnClicked);
                data.setBestScore(bestScore);
                data.setLastScore(lastScore);
				data.setCoins(coins);
				data.setMicrochips(microchips);
                data.setAchievementsUnlocked(achievements);
                bf.Serialize(file, data);
            }
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }


    }
    //                            .............here we get data from save
    public void Load()
    {
        FileStream file = null;
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);

        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    //for resetting the gameManager

    public void ResetGameManager()
    {
        isGameStartedFirstTime = false;
        isMusicOn = true;

        bestScore = lastScore = 0;

        achievements = new bool[5];//if you want ot add more achievements change the value here
        for (int i = 0; i < achievements.Length; i++)
        {
            achievements[i] = false;
        }

        fbBtnClicked = twitterBtnClicked = false;
            

        data = new GameData();

        data.setIsGameStartedFirstTime(isGameStartedFirstTime);
        data.setMusicOn(isMusicOn);
        data.setFbClick(fbBtnClicked);
        data.setTwitterClick(twitterBtnClicked);
        data.setBestScore(bestScore);
        data.setLastScore(lastScore);
		data.setCoins(coins);
		data.setMicrochips(microchips);
        data.setAchievementsUnlocked(achievements);
        Save();
        Load();

        Debug.Log("GameManager Reset");
    }


}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private bool fbBtnClicked, twitterBtnClicked;
	private int bestScore, lastScore, coins, microchips;
    private bool[] achievements;

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;

    }

    public bool getIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;

    }
    //                                                                    ...............music
    public void setMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;

    }

    public bool getMusicOn()
    {
        return this.isMusicOn;

    }
    //                                                                      .......music
        
    //....................................................for fb btn
    public void setFbClick(bool fbBtnClicked)
    {
        this.fbBtnClicked = fbBtnClicked;

    }

    public bool getFbClick()
    {
        return this.fbBtnClicked;

    }

    //....................................................for twitter btn
    public void setTwitterClick(bool twitterBtnClicked)
    {
        this.twitterBtnClicked = twitterBtnClicked;

    }

    public bool getTwitterClick()
    {
        return this.twitterBtnClicked;

    }
    //best score
    public void setBestScore(int bestScore)
    {
        this.bestScore = bestScore;
    }

    public int getBestScore()
    {
        return this.bestScore;
    }
    //last score
    public void setLastScore(int lastScore)
    {
        this.lastScore = lastScore;
    }

    public int getLastScore()
    {
        return this.lastScore;
    }

	public void setCoins(int coins)
	{
		this.coins = coins;
	}

	public int getCoins()
	{
		return this.coins;
	}

	public void setMicrochips(int microchips)
	{
		this.microchips = microchips;
	}

	public int getMicrochips()
	{
		return this.microchips;
	}

    //achievements unlocked
    public void setAchievementsUnlocked(bool[] achievements)
    {
        this.achievements = achievements;
    }

    public bool[] getAchievementsUnlocked()
    {
        return this.achievements;
    }


}