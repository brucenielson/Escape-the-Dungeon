using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour {

	public Transform player;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		//Debug.Log(anim.ToString());
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (anim.ToString ());
		//Debug.Log ("Start Update");
		Vector3 direction = player.position - this.transform.position;
		//Debug.Log ("Distance: " + direction.magnitude.ToString ());
		float angle = Vector3.Angle (direction, this.transform.forward);
		//Debug.Log ("Angle: " + angle.ToString ());
		//Debug.Log(anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"));

		// Get top animation currently running
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		int damageState = Animator.StringToHash ("Damage");
		int walkState = Animator.StringToHash ("Walk");
		int idleState = Animator.StringToHash ("Idle");
		int attackState = Animator.StringToHash ("Attack");

		if (anim.GetBool("Hit") && !stateInfo.IsName ("Damage")) {
			//Debug.Log("Hit!");
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("Hit", true);
		}
		else if (Vector3.Distance (player.position, this.transform.position) < 10 && angle < 120 && !stateInfo.IsName ("Attack") && !stateInfo.IsName ("Damage")) {

			direction.y = 0;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			anim.SetBool ("isIdle", false);
			if (direction.magnitude > 2) {
				//this.transform.Translate (0, 0, 0.03f);
				//Debug.Log("Walking!");
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isAttacking", false);
				anim.SetBool ("Hit", false);
			} else {
				//Debug.Log("Attacking!");
				anim.SetBool ("isAttacking", true);
				anim.SetBool ("isWalking", false);
				anim.SetBool ("Hit", false);
			}
		} 
		else {
			//Debug.Log("Idle!");
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
			anim.SetBool ("Hit", false);
		}
	}
}
