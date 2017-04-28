using UnityEngine;
using System.Collections;

public class MouseController : InputController {
	protected override void Update() {
		base.Update ();

		if (Input.GetButtonDown ("Fire1")) {
			shootWitchBullet ();
		} else if (Input.GetButtonDown ("Fire2")) {
			shootRavenBullet ();
		} else if (Input.GetKeyDown (KeyCode.A)) {
			StartGame ();
		} else if (Input.GetKeyDown (KeyCode.R)) {
			RestartLevel ();
		}else if (Input.GetKeyDown (KeyCode.L)) {
			if (Cursor.lockState == CursorLockMode.None) {
				Cursor.lockState = CursorLockMode.Confined;
			} else {
				Cursor.lockState = CursorLockMode.None;
			}
		}
	}
}
