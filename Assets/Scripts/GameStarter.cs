using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter: MonoBehaviour {
	//Class attached to the lion statue game object.
	private AudioSource startSong;
	void Start() {
		startSong = GetComponent < AudioSource > ();
	}

	void OnTriggerEnter(Collider other) {
		startSong.Play(); //When the player hits the statue, the starting song plays.
	}
}