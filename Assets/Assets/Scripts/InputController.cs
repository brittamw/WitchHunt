using UnityEngine;
using System.Collections;

public abstract class InputController : MonoBehaviour {

	public GameManager gameManager;

	protected virtual void Update() {
		
	}

	protected virtual void StartGame() {
		gameManager.goToStart ();
	}
}
