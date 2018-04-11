using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This script creates the clone of required objects and provide them as required
/// </summary>
/// 

public class ObjectPooling : MonoBehaviour
{

    public static ObjectPooling instance;

    public GameObject scoreEffect; //ref to score effect prefabs
    public GameObject platform;

    public int count = 5; //total clones of each object to be spawned

    List<GameObject> ScoreEffect = new List<GameObject>();
    List<GameObject> SpawnPlatform = new List<GameObject>();

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        //ScoreEffect
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(scoreEffect);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            ScoreEffect.Add(obj);
        }
        //Platform
        for (int i = 0; i < 1; i++)
        {
            GameObject obj = Instantiate(platform);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            SpawnPlatform.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //method which is used to call from other scripts to get the clone object
   
    //ScoreEffect
    public GameObject GetScoreEffect()
    {
        for (int i = 0; i < ScoreEffect.Count; i++)
        {
            if (!ScoreEffect[i].activeInHierarchy)
            {
                return ScoreEffect[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(scoreEffect);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        ScoreEffect.Add(obj);
        return obj;
    }

    //Platform
    public GameObject GetPlatfrom()
    {
        for (int i = 0; i < SpawnPlatform.Count; i++)
        {
            if (!SpawnPlatform[i].activeInHierarchy)
            {
                return SpawnPlatform[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(platform);
        obj.transform.parent = gameObject.transform;
        obj.SetActive(false);
        SpawnPlatform.Add(obj);
        return obj;
    }

}