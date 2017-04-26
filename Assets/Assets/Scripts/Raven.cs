using UnityEngine;
using System.Collections;

public class Raven : Enemy {

	public Material deadMaterial;

	public override void DoAction(bool rightAction) {
		if (rightAction) {
			alive = false;            
			animator.speed = 0;
			skinnedRenderer.material = deadMaterial;
			audioSource.clip = rightActionAudio;
			enemey.useGravity = true;
			enemey.constraints = RigidbodyConstraints.None;
			deadEffect.Emit(1);
			//Destroy (gameObject,3f);
		} else {
			if (audioSource != null) {
				audioSource.clip = wrongActionAudio;
			}
		}
		if (audioSource != null) {
			audioSource.Play ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (alive) {
		base.OnTriggerEnter (other);
			if (other.CompareTag ("BulletForRaven")) {
				DoAction (true);
			} else if (other.CompareTag ("BulletForWitch")) {
				DoAction (false);
			}
		}
	}
}
