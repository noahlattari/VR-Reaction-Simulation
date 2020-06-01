using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordLogic: MonoBehaviour {
	//Class attached to the player/controller/sword

	private AudioSource clashingSound;
	private Text introText;
	private Text comboText;
	private GameObject cameraRig;
	private GameObject battlePlace;
	private GameObject Skeleton;
	static Animator animator;

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;
	
	void Start() {
        //Get reference to components on the sword.
		trackedObject = GetComponentInParent < SteamVR_TrackedObject > ();
		clashingSound = GetComponent < AudioSource > ();
	}

	void updateComboText() {
        //Called when the sword starts the game.
		comboText.text = "Combo: " + count.ToString();
	}

	void OnTriggerEnter(Collider other) {
        //Called when the sword touches another collider (usually the skeleton)
		if (other.gameObject.tag == "GameStarter") {
            //GameStarter is an object the player hits when they are ready to start the game.
			introText.enabled = false; //Turn off the controls text
			clashingSound.Play(); 
            
            //Initiate SteamVR
			device = SteamVR_Controller.Input((int) trackedObject.index);
			device.TriggerHapticPulse(64000); //Vibrate

			cameraRig.transform.position = 
                new Vector3(battlePlace.transform.position.x, battlePlace.transform.position.y, battlePlace.transform.position.z); //Move the Camera to the battle arena.

			this.gameObject.GetComponent < SpawnAlgorithm > ().isSpawing = true; //Lets SpawnAlgorithm class know the skeletons can now spawn.

		}

		if (other.gameObject.tag == "enemy") {
            //Enemy is any skeleton object.
			clashingSound.Play();
			count++;
			updateComboText(); //update comboText

			device = SteamVR_Controller.Input((int) trackedObject.index);
			device.TriggerHapticPulse(64000); //Vibrate

			other.gameObject.GetComponent < SkeletonAI > ().skeletonDeath(); //Let SkeletonAI know the hit skeleton is dead.
		}
	}

}