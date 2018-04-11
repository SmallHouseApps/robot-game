using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

    public GameObject mainCamera;            //ref to camera
    public float distanceBtwPlatform = 2.5f; //min ditance between platform
    public Transform startingPos;            //ref to start position from where spawning start

    private float lastYValue;                //ref to the last platfrom Y value

	// Use this for initialization
	void Start ()
    {
        StartSpawn();
        lastYValue = mainCamera.transform.position.y; //default value
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check if the camera position and last value difference is more or equal to the limit
        if (mainCamera.transform.position.y - lastYValue >= distanceBtwPlatform)
        {
            //if yes then spawn the platform
            GameObject platform = ObjectPooling.instance.GetPlatfrom();
            platform.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            platform.GetComponent<PlatformController>().Settings();
            platform.SetActive(true);
            lastYValue = mainCamera.transform.position.y; //change the last y value
        }
	}

    /// <summary>
    /// Method which spawns the strting platforms
    /// </summary>
    void StartSpawn()
    {
        for (int i = 0; i < 4; i++)
        {
            //startingPos.position = new Vector3(startingPos.position.x, startingPos.position.y + 2.5f * i);
            GameObject platform = ObjectPooling.instance.GetPlatfrom();
            platform.transform.position = new Vector3(transform.position.x, startingPos.position.y + 2.5f * i, 0);
            platform.GetComponent<PlatformController>().Settings();
            platform.SetActive(true);
        }
    }

}
