using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAlgorithm: MonoBehaviour {
	//Class attached to the skelton prefab.

	private Slider HealthBar;
	private GameObject player;
	private GameObject GameStarter;
	private float InitialTimer = 5.0f;
	private float timer = 0.0f;
	private float decayFactor = 1.0f;
	private bool isSpawing = false;

	private GameObject fire1; //Ball of fires which indicate where the skeletons spawn from
	private GameObject fire2;
	private GameObject fire3;

	void Update() {
		if (isSpawing) { //If skeletons are spawning, start a timer
			timer = timer - Time.deltaTime;
			if (timer < 0) { //If that timer goes below 0,
				updatedecay();
				startTimer(InitialTimer * decayFactor);
				float random = randomGenerate(); //Generate a random float between 0 and 3
				SpawnGuy(random); //Spawn a skeleton in 1 of 4 positions randomly
			}
		}
	}

	float randomGenerate() {
		float random = Random.Range(0.0f, 3.0f);
		return random;
	}

	public void updatedecay() {
		if (decayFactor >= 0.20) {
			decayFactor = decayFactor / 1.5f;
		}
	}

	public void startTimer(float time) {
		isSpawing = true;
		timer = time;
	}

	void SpawnGuy(float Spawnlocation) {
		GameObject skeleInstance = Instantiate(skel);
		createSkeletonAI(skeleInstance.GetComponentInChildren < skeletonSwordHit > ()); //Load the new skeleton with specific values.
		skeleInstance.SetActive(true);

		//TODO: Maybe a switch statement makes more sense here?
		if (Spawnlocation <= 1.0) {
			skeleInstance.transform.position = fire1.transform.position; //Spawn a skeleton at the fire
		}

		else if (Spawnlocation > 1.0 && Spawnlocation <= 2.0) {
			skeleInstance.transform.position = fire2.transform.position; 
		}

		else { //Spawnlocation > 2.0
			skeleInstance.transform.position = fire3.transform.position;
		}
	}

	void createSkeletonAI(SkeletonAI skeletonInstance) {
		skeletonInstance.ground = ground;
		skeletonInstance.GameStarter = GameStarter;
		skeletonInstance.player = player;
		skeletonInstance.healthbar = HealthBar;

	}
}