using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	int startingHealth = 0;
    public int currentScore;
	public Text score;
    public AudioClip deathClip;
	public EnemyManager enemyManager;
    
    // Use this for initialization
	void Awake () {
		currentScore = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (score != null) {
			score.text = currentScore.ToString ();
		}	
    }

    public void Score(int amount)
    {
		currentScore += amount;
    }
}
