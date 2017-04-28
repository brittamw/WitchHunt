using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public abstract class InputController : MonoBehaviour {

	public GameObject ravenBullet;
	public GameObject witchBullet;
	public float bulletSpeed = 1000f;
	public float bulletLife = 3f;

	public GameManager gameManager;

	protected virtual void Update() {
		
	}

	public virtual void StartGame() {
		gameManager.startGame ();
	}

	public virtual void RestartLevel() {
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}

	public virtual void shootRavenBullet() {
		FireBullet (ravenBullet);
	}

	public virtual void shootWitchBullet() {
		FireBullet (witchBullet);
	}

	private void FireBullet(GameObject bullet)
	{
		GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
		bulletClone.SetActive(true);
		Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
		rb.AddForce(-bullet.transform.forward * bulletSpeed);
		Destroy(bulletClone, bulletLife);
	}
}
