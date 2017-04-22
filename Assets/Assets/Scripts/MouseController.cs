using UnityEngine;
using System.Collections;

public class MouseController : InputController {
	protected override void Update() {
		base.Update ();

		if (Input.GetButtonDown("Fire1")) {
			StartGame ();
		} else if (Input.GetButtonDown("Fire2")) {

		}
	}
}
