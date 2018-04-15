using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour {

	public Button closeBtn, moneyTap, bonusTap, decorTap;
	public GameObject shopPanel, moneyPanel, decorPanel;


	// Use this for initialization
	void Start () {
		//closeBtn.GetComponent<Button>().onClick.AddListener(() => { CloseBtn(); });  


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CloseBtn()
	{
		//shopPanel.SetActive(false);
	}

}
