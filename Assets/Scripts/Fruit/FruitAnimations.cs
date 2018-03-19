using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitAnimations : MonoBehaviour {

	Animator anim;
	public Rigidbody2D fruit;

	void Start () {
		anim = GetComponent<Animator>();
		anim.SetBool ("vinefruit", true);
	}

	void Update () {
		if (fruit.velocity.y < 0) {
			anim.SetBool ("vinefruit", false);
			anim.SetBool ("fallingfruit", true);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Ground")) 
		{
			anim.SetBool ("fallingfruit", false);
			anim.SetTrigger ("landed");
			anim.SetBool ("onGround", true);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag ("Ground")) 
		{
			anim.SetBool ("fallingfruit", true);
			anim.SetBool ("onGround", false);
		}
	}
}
