using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	public bool alive;
	public Rigidbody enemey;
	public AudioClip rightActionAudio;
	public AudioClip wrongActionAudio;
	public AudioSource audioSource;

	public EnemyManager enemyManager;
	public PlayerHealth playerHealth;

	Vector3 velocity = Vector3.zero;
	public float smoothTime = 10F;
	GameObject playerTarget;
	GameObject mountainTarget;
	GameObject currentTarget;
	float maxSpeed = 130f;

    void Start () {
		enemyManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		playerHealth = GameObject.FindGameObjectWithTag ("PlayerHealth").gameObject.GetComponent<PlayerHealth> ();
		playerTarget = GameObject.FindGameObjectWithTag ("PlayerTarget");
		mountainTarget = GameObject.FindGameObjectWithTag ("MountainTarget");
		currentTarget = playerTarget;
		alive = true;
		enemey = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(currentTarget.transform.position), Time.deltaTime);
			//transform.LookAt(currentTarget.transform);
			//transform.position = Vector3.MoveTowards (transform.position, target.transform.position, maxSpeed * Time.deltaTime);
			transform.position = Vector3.SmoothDamp (transform.position, currentTarget.transform.position, ref velocity, smoothTime, maxSpeed);
		}
	}

	public abstract void DoAction (bool rightAction);

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("MountainTarget")) {
			enemyManager.enemyTooNear (this);
		} else if (other.CompareTag ("PlayerTarget")) {
			currentTarget = mountainTarget;
		}
	}
}
