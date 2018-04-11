using UnityEngine;
using System.Collections;

/// <summary>
/// Scritp which make the clouds move
/// </summary>
public class CloudMove : MonoBehaviour {

    public float speed = 0.25f;
    private Vector2 direction;

	// Use this for initialization
	void Start ()
    {
        direction = Vector2.right;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x >= 13f)
            direction = Vector2.left;
        else if (transform.position.x <= 0f)
            direction = Vector2.right;

        transform.Translate(direction * Time.deltaTime * speed);
    }
}
