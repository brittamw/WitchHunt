using UnityEngine;
using System.Collections;

public class BadEnemy : Enemy {
	
	public override void DoAction(bool rightAction) {
		if (rightAction) {
			enemey.isKinematic = false;
			Vector3 force = new Vector3 (-5, 40, 50);
			enemey.AddForce (force, ForceMode.Impulse);
			alive = false;
			audioSource.clip = rightActionAudio;
		} else {
			alive = false;
			audioSource.clip = wrongActionAudio;
		}
		if (audioSource != null) {
			audioSource.Play ();
		}
	}
}
