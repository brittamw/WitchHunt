using UnityEngine;
using System.Collections;

public class GoodEnemy : Enemy {
    Animator animator;
	public override void DoAction(bool rightAction) {
		if (rightAction) {
            animator = GetComponent<Animator>();
			alive = false;
            
			audioSource.clip = rightActionAudio;
			playerHealth.TakeDamage (-2);
            animator.SetTrigger("freuen");
			Destroy (this.gameObject, 1f);
		} else {
			enemey.isKinematic = false;
			Vector3 force = new Vector3 (-5, 40, 50);
			enemey.AddForce (force, ForceMode.Impulse);
			alive = false;
			if (audioSource != null) {
				audioSource.clip = wrongActionAudio;
			}
			playerHealth.TakeDamage (10);
		}
		if (audioSource != null) {
			audioSource.Play ();
		}
	}
}
