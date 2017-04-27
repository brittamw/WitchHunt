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

	float currentNavSpeed;
	public int spawnTimeWitchDistance = 250;
	public int spawnTimeRavenDistance = 250;
	int gameLevel;

	public GameObject spawnPointWitch1;
	public GameObject spawnPointWitch2;
	public GameObject spawnPointRaven1;
	public GameObject spawnPointRaven2;

	public Light witchLight;

	// Use this for initialization
	void Start () {
		gameRunning = false;
		firstStart = true;
		allEnemies = new ArrayList ();
		currentNavSpeed = 3.5f;
		gameLevel = 1;       
	}

	// Update is called once per frame
	void Update () {
		if (gameRunning) {
			if (firstStart) {
				InvokeRepeating ("makeGameHarder", 5f, 5f);
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
				witchLight.intensity = witchLight.intensity - 2f;
			}
		}
	}

	void DoActionToEnemy(Enemy enemy, bool rightAction) {
		enemy.DoAction (rightAction);
	}

	void createWitch() {
		Transform spawnPoint;
		float randomForPosition = Random.value*2f;
		if (Mathf.Floor (randomForPosition) == 0) {
			spawnPoint = spawnPointWitch1.transform;
		} else {
			spawnPoint = spawnPointWitch2.transform;
		}
			
		witchLight.intensity = 15f;

		float randomForType = Random.value*2f;
		Enemy newEnemyObject = (Enemy) Instantiate (witch, spawnPoint.position, spawnPoint.rotation);
		newEnemyObject.enabled = true;

		allEnemies.Add (newEnemyObject);
	}

	void createRaven() {
		Transform spawnPoint;
		float randomForPosition = Random.value*2f;
		if (Mathf.Floor (randomForPosition) == 0) {
			spawnPoint = spawnPointRaven1.transform;
		} else {
			spawnPoint = spawnPointRaven2.transform;
		}

		Enemy newEnemyObject = (Enemy) Instantiate (raven, spawnPoint.position, spawnPoint.rotation);

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
		/*spawnTime = spawnTime - (75/gameLevel);
		if (spawnTime < 30) {
			spawnTime = 30;
		}*/

		//currentNavSpeed = currentNavSpeed + (2f/gameLevel);
		//Debug.Log ("Game is now at level: " + gameLevel + " speed=" + currentNavSpeed + " spawntime: " + spawnTime);
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
