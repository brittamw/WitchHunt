using UnityEngine;
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
	public Light mountainHealthLight;

	public Light healthLight1;
	public Light healthLight2;
	public Light healthLight3;


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
			mountainHealthLight.intensity = mountainHealthLight.intensity + 5;

			if (currentHealth == 1) {
				healthLight1.enabled = true;
			} else if (currentHealth == 2) {
				healthLight2.enabled = true;
			} else if (currentHealth == 3) {
				healthLight3.enabled = true;
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
