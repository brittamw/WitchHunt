using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public Raven raven;
	public Witch witch;

	ArrayList allEnemies;
    
	public float timeBetweenWitchSpawning;
	public float timeBetweenRavenSpawning;

	bool gameRunning;
	bool firstStart;

	public GameManager gameManager;
	public PlayerHealth playerHealth;
	public MountainHealth mountainHealth;

	public int spawnTimeWitchDistance = 250;
	public int spawnTimeRavenDistance = 250;

	public int gameLevel;
	public float gameLevelSteps = 30f;

	public int maxLevel;

	public GameObject spawnPointWitch1;
	public GameObject spawnPointWitch2;
	public GameObject spawnPointWitch3;
	public GameObject spawnPointRaven1;
	public GameObject spawnPointRaven2;
	public GameObject spawnPointRaven3;
	public GameObject spawnPointRaven4;
	public GameObject spawnPointRaven5;
	public GameObject spawnPointRaven6;
	public GameObject spawnPointRaven7;

	public GameObject fireworks;

	public Light witchLight;

	// Use this for initialization
	void Start () {
		gameRunning = false;
		firstStart = true;
		allEnemies = new ArrayList ();
		gameLevel = 1;       
	}

	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			if (firstStart) {
				InvokeRepeating ("makeGameHarder", gameLevelSteps, gameLevelSteps);
				firstStart = false;
			}
			if (timeBetweenWitchSpawning == 0) {
				Invoke ("createWitch", 0f);
				timeBetweenWitchSpawning = spawnTimeWitchDistance;
			}
			timeBetweenWitchSpawning--;
			if (timeBetweenRavenSpawning == 0) {
				Invoke ("createRaven", 0f);
				timeBetweenRavenSpawning = spawnTimeRavenDistance;
			}
			timeBetweenRavenSpawning--;
			if (witchLight.intensity > 0) {
				witchLight.intensity = witchLight.intensity - 1f;
			}
		}
	}

	void DoActionToEnemy(Enemy enemy, bool rightAction) {
		enemy.DoAction (rightAction);
	}

	void createWitch() {
		Transform spawnPoint;
		float randomForPosition = Random.value*3f;
		if (Mathf.Floor (randomForPosition) == 0) {
			spawnPoint = spawnPointWitch1.transform;
		} else if (Mathf.Floor (randomForPosition) == 1 && gameLevel >= 3) {
			spawnPoint = spawnPointWitch3.transform;
		} else {
			spawnPoint = spawnPointWitch2.transform;
		}
			
		witchLight.intensity = 25f;

		float randomForType = Random.value*2f;
		Enemy newEnemyObject = (Enemy) Instantiate (witch, spawnPoint.position, spawnPoint.rotation);
		newEnemyObject.enabled = true;

		newEnemyObject.audioSource.Play ();

		if (gameLevel == maxLevel) {
			newEnemyObject.maxSpeed = 15f;
		}

		allEnemies.Add (newEnemyObject);
	}

	void createRaven() {
		
		Transform spawnPoint;
		float randomForPosition = Random.value*7f;
		if (Mathf.Floor (randomForPosition) == 0) {
			spawnPoint = spawnPointRaven1.transform;
		} else if (Mathf.Floor (randomForPosition) == 1) {
			spawnPoint = spawnPointRaven2.transform;
		} else if (Mathf.Floor (randomForPosition) == 2) {
			spawnPoint = spawnPointRaven3.transform;
		} else if (Mathf.Floor (randomForPosition) == 3) {
			spawnPoint = spawnPointRaven4.transform;
		} else if (Mathf.Floor (randomForPosition) == 4) {
			spawnPoint = spawnPointRaven5.transform;
		} else if (Mathf.Floor (randomForPosition) == 5) {
			spawnPoint = spawnPointRaven6.transform;
		} else {
			spawnPoint = spawnPointRaven7.transform;
		}

		float random = Random.value*20f - 10f;

		spawnPoint.position = new Vector3 (spawnPoint.position.x + random, spawnPoint.position.y + random, spawnPoint.position.z + random);
		//spawnPoint.position.x = spawnPoint.position.x + random;
		//spawnPoint.position.y = spawnPoint.position.y + random;
		//spawnPoint.position.z = spawnPoint.position.z + random;

		Enemy newEnemyObject = (Enemy) Instantiate (raven, spawnPoint.position, spawnPoint.rotation);
		newEnemyObject.enabled = true;

		//newEnemyObject.audioSource.Play ();

		allEnemies.Add (newEnemyObject);
	}

	public void enemyTooNear(Enemy enemy) {
		mountainHealth.TakeDamage (1);
		//Destroy (enemy.gameObject);
	}

	public void makeGameHarder() {
		if (gameLevel < maxLevel) {
			gameLevel++;
			spawnTimeWitchDistance = spawnTimeWitchDistance - 50;
			if (spawnTimeWitchDistance < 60) {
				spawnTimeWitchDistance = 60;
			}

			spawnTimeRavenDistance = spawnTimeRavenDistance - 30;
			if (spawnTimeRavenDistance < 25) {
				spawnTimeRavenDistance = 25;
			}
		} else {
			spawnTimeWitchDistance = 20;
			spawnTimeRavenDistance = 15;
		}

		Debug.Log ("Game is now at level: " + gameLevel + " witch=" + spawnTimeWitchDistance + " raven: " + spawnTimeRavenDistance);
	}

	public void startGame() {
		gameRunning = true;
	}

	public void endGame() {
		gameRunning = false;
		foreach(Enemy e in allEnemies) {
			if (e != null && e.gameObject != null) {
				if (e.audioSource != null) {
					e.audioSource.enabled = false;
				}
			}
		}
		fireworks.SetActive (true);
		gameManager.gameOver ();
	}
}
