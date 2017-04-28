using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCampfire : MonoBehaviour {

	public GameObject particles;
	public GameManager gameManager;
	public GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("BulletForRaven")) {
			particles.SetActive (false);
			if (!gameManager.gameStarted) {
				gameManager.startGame ();
				canvas.SetActive (false);
			}
		} else if (other.CompareTag ("BulletForWitch")) {
			particles.SetActive (true);
		}
	}
}
