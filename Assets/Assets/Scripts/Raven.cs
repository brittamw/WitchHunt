using UnityEngine;
using System.Collections;

public class Raven : Enemy {

	public Material deadMaterial;

	protected void Start() {
		base.Start ();
		maxSpeed = 6f;
		currentTarget = playerTarget;
	}

	public override void DoAction(bool rightAction) {
		if (rightAction) {
			alive = false;            
			animator.speed = 0;
			foreach (SkinnedMeshRenderer r in skinnedRenderer) {
				r.material = deadMaterial;
			}
			audioSource.clip = rightActionAudio;
			enemey.useGravity = true;
			enemey.constraints = RigidbodyConstraints.None;
			deadEffect.Emit(1);
			Destroy (gameObject,3f);
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
		//base.OnTriggerEnter (other);
			if (other.CompareTag ("PlayerTarget")) {
				playerHealth.TakeDamage (1);
				Destroy (gameObject);
			} else if (other.CompareTag ("BulletForRaven")) {
				DoAction (true);
			} else if (other.CompareTag ("BulletForWitch")) {
				DoAction (false);
			}
		}
	}
}
