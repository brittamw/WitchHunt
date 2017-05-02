﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MountainHealth : MonoBehaviour {

    int startingHealth = 0;
	int deadHealth = 3;
    public int currentHealth;

	public Text healthPoints;


    // public Slider healtSlider; // falls wir einen EP-Balken haben wollen
    public AudioClip deathClip;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public EnemyManager enemyManager;

	public GameObject healthLight1;
	public GameObject healthLight2;
	public GameObject healthLight3;


	public bool alive;
    bool damaged;
    
    // Use this for initialization
	void Awake () {
        currentHealth = startingHealth;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
       // Bei Schaden Bild rot aufleuchten lassen
		if (healthPoints != null) {
			healthPoints.text = currentHealth.ToString ();
		}
		if (damaged) {
			//damageImage.color = flashColour;
		} else {
			//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;	
    }

    public void TakeDamage(int amount)
    {
		if (alive) {
			damaged = true;
			currentHealth += amount;

			if (currentHealth == 1) {
				healthLight1.SetActive(true);
			} else if (currentHealth == 2) {
				healthLight2.SetActive(true);
			} else if (currentHealth == 3) {
				healthLight3.SetActive(true);
				//TODO add Firework
			}

			if(currentHealth >= deadHealth && alive)
			{
				Die();
			}
		}
    }

    void Die()
    {
		alive = false;
		enemyManager.endGame ();
    }
}
