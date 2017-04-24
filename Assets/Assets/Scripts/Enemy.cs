﻿using UnityEngine;
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
	GameObject mountainTopTarget;
	GameObject mountainBottomTarget;
	GameObject currentTarget;
	float maxSpeed = 5f;

	protected MeshRenderer[] renderer;
	protected ParticleSystem deadEffect;


    void Start () {
		enemyManager = GameObject.FindGameObjectWithTag ("EnemyManager").gameObject.GetComponent<EnemyManager> ();
		playerHealth = GameObject.FindGameObjectWithTag ("PlayerHealth").gameObject.GetComponent<PlayerHealth> ();
		playerTarget = GameObject.FindGameObjectWithTag ("PlayerTarget");
		mountainTopTarget = GameObject.FindGameObjectWithTag ("MountainTopTarget");
		mountainBottomTarget = GameObject.FindGameObjectWithTag ("MountainBottomTarget");
		currentTarget = playerTarget;
		alive = true;
		enemey = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
		renderer = GetComponentsInChildren<MeshRenderer> ();
		deadEffect = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			//transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation(currentTarget.transform.position), Time.deltaTime);
			Vector3 targetDir = currentTarget.transform.position - transform.position;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, 0.5f * Time.deltaTime, 0f);
			transform.rotation = Quaternion.LookRotation (newDir);

			//transform.LookAt(currentTarget.transform);
			transform.position = Vector3.MoveTowards (transform.position, currentTarget.transform.position, maxSpeed * Time.deltaTime);
			//transform.position = Vector3.SmoothDamp (transform.position, currentTarget.transform.position, ref velocity, smoothTime, maxSpeed);
		}
	}

	public abstract void DoAction (bool rightAction);

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("PlayerTarget")) {
			currentTarget = mountainBottomTarget;
		} else if (other.CompareTag ("MountainBottomTarget")) {
			currentTarget = mountainTopTarget;
		} else if (other.CompareTag ("MountainTopTarget")) {
			enemyManager.enemyTooNear (this);
		} 
	}
}