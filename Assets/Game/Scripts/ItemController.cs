using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

	private const byte COIN = 1, MICROCHIP = 2;
	private bool touch = false;
	private string detectedObject = "";

	#region Public
	public byte bonusType;
	private Vector2 direction;       //direction of movement
	private Rigidbody2D myBody;      //for rigidbody2D
	#endregion


	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D>(); //get the rigidbody2D component
		myBody.isKinematic = true;
		myBody.velocity = new Vector2(0,0);
		detectedObject = "";
	}
	
	// Update is called once per frame
	void Update () {
		detectedObject = "";
	}

	void OnTriggerEnter2D(Collider2D other) {   
		//check the tag and compare the object name with the detecteObjet
		switch (bonusType) { 
			case COIN: 
			if (other.CompareTag ("Player") && other.gameObject.name != detectedObject) {   
					detectedObject = other.gameObject.name;
					GameManager.instance.coins++;
					print ("On COIN - Player " + GameManager.instance.coins);
					gameObject.SetActive (false);
				}
				break;
			case MICROCHIP: 
			if (other.CompareTag ("Player") && other.gameObject.name != detectedObject) {   
					detectedObject = other.gameObject.name;
					GameManager.instance.microchips++;
					print ("On MICROCHIP - Player " + GameManager.instance.microchips);
					gameObject.SetActive (false);
				}
				break;
			default :
				break;
		}
	}

}
