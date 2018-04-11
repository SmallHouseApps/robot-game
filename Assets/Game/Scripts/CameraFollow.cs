using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Script attached to the camera to follow player object
/// </summary>
/// 
public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    private Vector2 velocity;
    private GameObject player; //ref to the player object in the scene
    private float distance; //distance between camera and player
    private bool playerGot = false;   //check if player is available

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }
    //method which will be called by hexaplayer when the hexaplayer spawns
    public void PlayerSettings()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //gets the distance between player and camera , we need this distance so to maintain it in the game
        distance = (player.transform.position.y - transform.position.y);
        playerGot = true;
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.instance.gameOver && playerGot)
            Movement();
    }
    //methode which make camera move smoothly
    void Movement()
    {
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - distance, ref velocity.y, 0.05f);
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
       
}