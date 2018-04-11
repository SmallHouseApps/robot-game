using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {

	private const byte ROCKET = 1, OIL = 2, SPRING = 3, SHIELD = 4, WEIGHT = 5;

	private bool detectedPlayer = false;


	#region Public
	public byte bonusType;
	public float state;
	#endregion

	#region Private
	private Vector2 direction;       //direction of movement
	private Rigidbody2D myBody;      //for rigidbody2D
	#endregion


	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D>(); //get the rigidbody2D component
		myBody.isKinematic = true;
		myBody.velocity = new Vector2(0,0);

	}
	
	// Update is called once per frame
	void Update () {
		//if (GameManager.instance.gameOver)
		//	return;	
		
	}

	void Movement() { // for Rocket
		
		direction = new Vector2(0, 1);
		// transform.localScale = new Vector2(0, direction.y);   не нужен 

		//add the velocity in given direction
		myBody.velocity = new Vector2(myBody.velocity.x, 6 * direction.y);

	}

	void OnTriggerEnter2D(Collider2D other) {   
		//check the tag and compare the object name with the detecteObjet
		switch (bonusType) { 
			case ROCKET: 
				if (other.CompareTag ("Player") && !detectedPlayer) {   
					detectedPlayer = true;
					// print ("On Trigger - Player");
					Movement ();
				}
				if (other.CompareTag ("Enemy") && detectedPlayer) {   
					// print ("On Trigger - Enemy");
					gameObject.SetActive (false);
					other.attachedRigidbody.gameObject.SetActive (false);
				}
				break;
			case OIL: 
				if (other.CompareTag ("Player") ) {   
					print ("OIL - On Trigger - Player");
					gameObject.SetActive (false);
				}
				break;
			case SPRING: 
				if (other.CompareTag ("Player") ) {   
					print ("SPRING - On Trigger - Player");
				}
				break;
			case SHIELD: 
				if (other.CompareTag ("Player") ) {   
					print ("SHIELD - On Trigger - Player");
					gameObject.SetActive (false);
				}
				break;
			case WEIGHT: 
				if (other.CompareTag ("Player") ) {   
					print ("WEIGHT - On Trigger - Player");
					gameObject.SetActive (false);
				}
				break;


			default :
				break;
		}
	}
}
