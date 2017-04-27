using UnityEngine;
using System.Collections;

public class Witch : Enemy {

	protected void Start() {
		base.Start ();
		maxSpeed = 3f;
	}

	public override void DoAction(bool rightAction) {
		if (rightAction) {
			alive = false;
			audioSource.clip = rightActionAudio;
			foreach (MeshRenderer r in renderer) {
				r.enabled = false;
			}
			foreach (SkinnedMeshRenderer r in skinnedRenderer) {
				r.enabled = false;
			}
			deadEffect.Emit(1);
			Destroy (gameObject,3f);
		} else {
			audioSource.clip = wrongActionAudio;
		}
		if (audioSource != null) {
			audioSource.Play ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (alive) {
			//base.OnTriggerEnter (other);
			if (other.CompareTag ("PlayerTarget")) {
				currentTarget = mountainBottomTarget;
			} else if (other.CompareTag ("MountainBottomTarget")) {
				currentTarget = mountainTopTarget;
			} else if (other.CompareTag ("MountainTopTarget")) {
				enemyManager.enemyTooNear (this);
			} else if (other.CompareTag ("BulletForRaven")) {
				DoAction (false);
			} else if (other.CompareTag ("BulletForWitch")) {
				DoAction (true);
			}
		}
	}
}
