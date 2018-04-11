using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

    public Color green, red;         //color code for platforms
    public GameObject[] enemies;     //0 for normal , 1 for fast and 2 for slow
	public GameObject[] bonuses; 
	public GameObject[] items;
    public Transform Spawner;        //ref spawn position for enemies
    public SpriteRenderer[] sprites; //ref to all the sprites of platform

    private Transform player;        //ref to player
	private GameObject lastEnemy, lastBonus, lastItem;    //ref to the enemy which was activated
    private string detectedObject;   //ref to the player name when detected
    private AudioSource sound;

    [HideInInspector]
    public ManageVariables vars;

    void OnEnable()
    {
        vars = Resources.Load("ManageVariablesContainer") as ManageVariables;
    }

    // Use this for initialization
    void Start ()
    {
        sound = GetComponent<AudioSource>(); //get the sound component
        player = FindObjectOfType<PlayerController>().transform; //get the player transform
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check if the distance between player and itself is ledd than -3
        if (transform.position.y - player.position.y <= -3f)
        {   //if yes then check if there was any lastenemy game object assigned
			IncrementScore();

            if(lastEnemy != null)
            {   //if yes the deactivate it and remove th referance
                lastEnemy.SetActive(false);
                lastEnemy = null;
            }
			if(lastBonus != null)
			{   //if yes the deactivate it and remove th referance
				lastBonus.SetActive(false);
				lastBonus = null;
			}
			if(lastItem != null)
			{   //if yes the deactivate it and remove th referance
				lastItem.SetActive(false);
				lastItem = null;
			}
            detectedObject = ""; //set the string to nothing
            gameObject.SetActive(false); //deactivate the gameobject
        }

		//if (transform.position.y - player.position.y <= -9f)
			//gameObject.SetActive(false); //deactivate the gameobject
	}
    /// <summary>
    /// Method which perform few settings when the gameobject is activated in the scene
    /// </summary>
    public void Settings()
	//TODO Добавить коэффициент сложности. Шанс выпадения бонуса. 
	{   //change color or all sprite to grey
        for (int i = 0; i < sprites.Length; i++)
        {
			sprites[i].color = red;
        }

		if (GameManager.instance.currentScore < 15) {
			int r = Random.Range (0, 10);
			float pos = Random.Range (-2f, 2f); 
        
			if (r >= 0 && r < 2) {  
				enemies [0].transform.position = new Vector3 (pos, Spawner.position.y);
				enemies [0].SetActive (true);
				lastEnemy = enemies [0];
			} else if (r >= 2 && r < 3) {   
				enemies [1].transform.position = new Vector3 (pos, Spawner.position.y);
				enemies [1].SetActive (true);
				lastEnemy = enemies [1];
			} else if (r >= 3 && r < 5) {
				enemies [2].transform.position = new Vector3 (pos, Spawner.position.y);
				enemies [2].SetActive (true);
				lastEnemy = enemies [2];
			} else {
				return;
			}

			r = Random.Range (0, 16);
			pos = Random.Range (-2f, 2f);

			// TODO  раставить шансы
			if (r >= 0 && r < 2) { 
				bonuses [0].transform.position = new Vector3 (pos, Spawner.position.y - 0.1f); //почти ок
				bonuses [0].SetActive (true);
				lastBonus = bonuses [0];
			} else if (r >= 2 && r < 3) {
				bonuses [1].transform.position = new Vector3 (pos, Spawner.position.y - 0.1f); //почти ок
				bonuses [1].SetActive (true);
				lastBonus = bonuses [1];
			} else if (r >= 3 && r < 4) {
				bonuses [2].transform.position = new Vector3 (pos, Spawner.position.y - 0.3f); //не ок
				bonuses [2].SetActive (true);
				lastBonus = bonuses [2];
			} else if (r >= 4 && r < 6) {
				bonuses [3].transform.position = new Vector3 (pos, Spawner.position.y - 0.1f); //ок
				bonuses [3].SetActive (true);
				lastBonus = bonuses [3];
			} else if (r >= 6 && r < 8) {
				bonuses [4].transform.position = new Vector3 (pos, Spawner.position.y - 0.2f); //ок 
				bonuses [4].SetActive (true);
				lastBonus = bonuses [4];
			} else {
				return;
			}
		} else if (GameManager.instance.currentScore >= 15 && GameManager.instance.currentScore < 50) {
			int r = Random.Range (0, 10);
			float pos = Random.Range (-2f, 2f); 

			if (r >= 0 && r < 3) {  
				enemies [0].transform.position = new Vector3 (pos, Spawner.position.y);
				enemies [0].SetActive (true);
				lastEnemy = enemies [0];
			} else if (r >= 3 && r < 5) {   
				enemies [1].transform.position = new Vector3 (pos, Spawner.position.y);
				enemies [1].SetActive (true);
				lastEnemy = enemies [1];
			} else if (r >= 5 && r < 7) {
				enemies [2].transform.position = new Vector3 (pos, Spawner.position.y);
				enemies [2].SetActive (true);
				lastEnemy = enemies [2];
			} else {
				return;
			}

			r = Random.Range (0, 16);
			pos = Random.Range (-2f, 2f);

			// TODO  раставить шансы
			if (r >= 0 && r < 2) { 
				bonuses [0].transform.position = new Vector3 (pos, Spawner.position.y - 0.1f); //почти ок
				bonuses [0].SetActive (true);
				lastBonus = bonuses [0];
			} else if (r >= 2 && r < 4) {
				bonuses [1].transform.position = new Vector3 (pos, Spawner.position.y - 0.1f); //почти ок
				bonuses [1].SetActive (true);
				lastBonus = bonuses [1];
			} else if (r >= 4 && r < 5) {
				bonuses [2].transform.position = new Vector3 (pos, Spawner.position.y - 0.3f); //не ок
				bonuses [2].SetActive (true);
				lastBonus = bonuses [2];
			} else if (r >= 5 && r < 6) {
				bonuses [3].transform.position = new Vector3 (pos, Spawner.position.y - 0.1f); //ок
				bonuses [3].SetActive (true);
				lastBonus = bonuses [3];
			} else if (r >= 6 && r < 8) {
				bonuses [4].transform.position = new Vector3 (pos, Spawner.position.y - 0.2f); //ок 
				bonuses [4].SetActive (true);
				lastBonus = bonuses [4];
			} else {
				return;
			}
		}

		int itemR = Random.Range (0, 20);
		float itemPos = Random.Range (-2f, 2f);

		// TODO  раставить шансы
		if (itemR >= 0 && itemR < 2) { 
			items [0].transform.position = new Vector3 (itemPos, Spawner.position.y - 0.1f); //почти ок
			items [0].SetActive (true);
			lastItem = items [0];
		} else if (itemR >= 2 && itemR < 3) {
			items [1].transform.position = new Vector3 (itemPos, Spawner.position.y - 0.1f); //почти ок
			items [1].SetActive (true);
			lastItem = items [1];
		} else {
			return;
		}

		return;
    }

    /// <summary>
    /// Method which detects the player
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {   //check the tag and compare the object name with the detecteObjet
        if (other.CompareTag("Player") && other.gameObject.name != detectedObject)
        {   //if above conditions are satisfied then assign the object name to detectedObject
            detectedObject = other.gameObject.name;
            //increase score , change color
			for (int i = 0; i < sprites.Length; i++)
			{   //then make the sprites to green
				sprites[i].color = green;
			}
        }
    }

	public void IncrementScore(){
		if (GameManager.instance.startGame == true)
		{   //check if game has started and the increase the score by 1
			GameManager.instance.currentScore++;
			NewHiScoreEffect();
		}
	}

    /// <summary>
    /// Method which show hi score effect when the last best score is defeated
    /// </summary>
    void NewHiScoreEffect()
    {
        if (GameManager.instance.currentScore > GameManager.instance.bestScore &&
            GameManager.instance.scoreEffect == 0)
        {
            GameManager.instance.scoreEffect = 1;
            //spawn the scoreEffect
            GameObject scoreEffect = ObjectPooling.instance.GetScoreEffect();
            scoreEffect.transform.position = transform.position;
            scoreEffect.SetActive(true);
            sound.PlayOneShot(vars.hiScore);
            scoreEffect.GetComponent<DeactivateObject>().Deactivate();
        }
    }

}
