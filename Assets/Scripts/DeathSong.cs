using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSong : MonoBehaviour {
	//Class attached to the ground game object.
    public AudioSource death;

	//Created because multiple audio files cannot be on the same object.
	
	void Start () {
        death = GetComponent<AudioSource>(); 
	}
}
