using UnityEngine;
using System.Collections;

public class Witch : Enemy {

	protected void Start() {
		base.Start ();
		maxSpeed = 5f;
		float random = Random.value*3f;
		if (Mathf.Floor (random) == 0) {
			currentTarget = openField1Target;
		} else if (Mathf.Floor (random) == 1) {
			currentTarget = openField2Target;
		} else {
			currentTarget = openField3Target;
		}
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
			playerHealth.Score (50);
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

			if (other.CompareTag ("OpenField1Target")) {
				currentTarget = mountainBottomTarget;
			} else if (other.CompareTag ("OpenField2Target")) {
				currentTarget = mountainBottomTarget;
			} else if (other.CompareTag ("OpenField3Target")) {
				currentTarget = mountainBottomTarget;
			} else if (other.CompareTag ("MountainBottomTarget")) {
				currentTarget = mountainTopTarget;
			} else if (other.CompareTag ("MountainTopTarget")) {
				enemyManager.enemyTooNear (this);
				Destroy (gameObject, 2f);
			} else if (other.CompareTag ("BulletForRaven")) {
				DoAction (false);
			} else if (other.CompareTag ("BulletForWitch")) {
				DoAction (true);
			}
		}
	}
}
