using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	public Raven raven;
	public Witch witch;

	ArrayList allEnemies;
    
	public float timeBetweenEnemySpawning;

	bool gameRunning;
	bool firstStart;

	public GameManager gameManager;
	public PlayerHealth playerHealth;

	float currentNavSpeed;
	int spawnTime;
	int gameLevel;

	public GameObject spawnPoint1;
	public GameObject spawnPoint2;

	// Use this for initialization
	void Start () {
		gameRunning = false;
		firstStart = true;
		allEnemies = new ArrayList ();
		currentNavSpeed = 3.5f;
		spawnTime = 250;
		gameLevel = 1;       
	}

	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			if (firstStart) {
				InvokeRepeating ("makeGameHarder", 5f, 5f);
				firstStart = false;
			}
			if (timeBetweenEnemySpawning == 0) {
				Invoke ("createEnemy", 0f);
				timeBetweenEnemySpawning = spawnTime;
			}
			timeBetweenEnemySpawning--;
		}
	}

	void DoActionToEnemy(Enemy enemy, bool rightAction) {
		enemy.DoAction (rightAction);
	}

	void createEnemy() {
		Transform spawnPoint;
		float randomForPosition = Random.value*2f;
		if (Mathf.Floor (randomForPosition) == 0) {
			spawnPoint = spawnPoint1.transform;
		} else {
			spawnPoint = spawnPoint2.transform;
		}

		float randomForType = Random.value*2f;
		Enemy newEnemyObject;
		if (Mathf.Floor(randomForType) == 0) {
			newEnemyObject = (Enemy) Instantiate (raven, spawnPoint.position, spawnPoint.rotation);
		} else {
			newEnemyObject = (Enemy) Instantiate (witch, spawnPoint.position, spawnPoint.rotation);
		}
		newEnemyObject.enabled = true;
		allEnemies.Add (newEnemyObject);
	}

	public void enemyTooNear(Enemy enemy) {
		playerHealth.TakeDamage (1);
		//Destroy (enemy.gameObject);
	}

	public float getCurrentNavSpeed() {
		return currentNavSpeed;
	}

	public void makeGameHarder() {
		gameLevel++;
		spawnTime = spawnTime - (75/gameLevel);
		if (spawnTime < 30) {
			spawnTime = 30;
		}

		currentNavSpeed = currentNavSpeed + (2f/gameLevel);
		Debug.Log ("Game is now at level: " + gameLevel + " speed=" + currentNavSpeed + " spawntime: " + spawnTime);
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
				e.DoAction (false);
			}
		}

		gameManager.gameOver ();
	}
}
