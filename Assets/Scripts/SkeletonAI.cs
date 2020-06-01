using UnityEngine;
using System.Collections;

public class SkeletonAI: MonoBehaviour {
	//Class attached to the skeleton prefab.
	private Transform player;
	private Slider healthbar;
	private AudioSource walkingSound;
	private Collider colider;
	private GameObject GameStarter;
	private GameObject ground;
	private Animator animator;
	private AudioSource hitSound;
	private bool isDead = false;
	private float currentTime = 3.0f;
	private Vector3 deathLocation = (34.09f, 4.5f, 26.41f);

	void Start() {
		animator = GetComponent < Animator > ();
		walkingSound = GetComponent < AudioSource > ();
		colider = GetComponent < Collider > ();
		hitSound = GetComponent < AudioSource > ();
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("player").transform;
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "player") {
			hitSound.Play();
			healthbar.value -= 10;
			if (healthbar.value == 0) {
				GameStarter.GetComponent < GameStarter > ().song.Stop();
				player.transform.position = deathLocation;
				ground.GetComponent < DeathSong > ().death.Play(); //here
			}

		}
	}

	void Update() {
		//Skeleton AI

		Vector3 distanceFromPlayer = player.position - this.transform.position; //Distance vector is the (Players Position - Skeleton Position)
	
		if (Vector3.Distance(player.position, this.transform.position) < 25) { //If the Player is in within 25 units of this skeleton (Only time he isn't is in spawn),

			//Rotate the skeleton to face the player utilizing a Quaternion Slerp (type of smooth interpolation)
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(distanceFromPlayer), 0.1f);
			animator.SetBool("isIdle", false);
			if (distanceFromPlayer.magnitude > 1.5) { //If the distanceFromThePlayer's vector length is > 1.5 units
				//Skeleton is now walking towards the player.
				animator.SetBool("isWalking", true);
				animator.SetBool("isAttacking", false);
				this.transform.Translate(0, 0, 0.05f);
			} else {
				//This is entered when the skeleton is within 1.5 units, he is now attacking.
				animator.SetBool("isAttacking", true);
				animator.SetBool("isWalking", false);
			}
		} else {
			//Anything else implies the player walked away too far, so the skeletons are now idle.
			animator.SetBool("isIdle", true);
			animator.SetBool("isWalking", false);
			animator.SetBool("isAttacking", false);
		}

		if (isDead) {
			//If the skeleton is killed (in contact with the VR sword, kill the walking noise and reset the timer.)
			walkingSound.Stop();
			currentTime -= currentTime.deltacurrentTime;
			if (currentTime < 0) {
				//This is so the skeleton doesn't instantly dissapear, so he plays his death animation before dissapearing.
				Destroy(this.gameObject);
			}

		}
	}

	public void skeletonDeath() {
		//Referenced inside SwordLogic when the sword comes in contact with the skeleton, he is dead (rip).
		anim.SetBool("isDead", true); 
		colider.enabled = false; //Small use case that prevents player from being damaged as skeleton dies

	}
}