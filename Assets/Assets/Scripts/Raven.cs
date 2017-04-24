using UnityEngine;
using System.Collections;

public class Raven : Enemy {
    Animator animator;
	public override void DoAction(bool rightAction) {
		if (rightAction) {
            //animator = GetComponent<Animator>();
			alive = false;            
            //animator.SetTrigger("freuen");
			audioSource.clip = rightActionAudio;
			foreach (MeshRenderer r in renderer) {
				r.enabled = false;
			}
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
		base.OnTriggerEnter (other);
		if (other.CompareTag ("BulletForRaven")) {
			DoAction (true);
		} else if (other.CompareTag ("BulletForWitch")) {
			DoAction (false);
		}
	}
}
