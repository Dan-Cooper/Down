using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSquisher : MonoBehaviour {

	public PlayerControler player;
	public bool isChild = false;
	public float cooldown = 0f;
	public bool coolStart = false;
	public BoxCollider2D squish;
	public Rigidbody2D rb;
	Animator anim;

	void Start () 
	{
		squish.enabled = false;
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		if (player.canInteractBox && Input.GetKeyDown ("e")) 
		{
			transform.parent = player.transform;
			isChild = true;
			coolStart = true;
			cooldownStart ();
			anim.SetBool ("floating", true);
			anim.SetTrigger ("startfloat");
		}
		if (isChild == true && Input.GetKeyDown ("e")) {
			if (cooldown == 0) 
			{
				transform.parent = null;
				isChild = false;
				anim.SetTrigger ("stopfloat");
				anim.SetBool ("floating", false);
			}
		}
		if (isChild == false) {
			squish.enabled = true;
		} else {
			squish.enabled = false;
		}
	}

	void cooldownStart()
	{
		if (coolStart == true)
		{
			cooldown++;
		}
		if (cooldown == 2)
		{
			cooldown = 0;
			coolStart = false;
		}
	}

	void FixedUpdate () {
		if (isChild) {
			rb.velocity = new Vector2 (player.GetComponent<Rigidbody2D> ().velocity.x, player.GetComponent<Rigidbody2D> ().velocity.y);
		}
	}

	/*void startFloat ()
	{
		if (anim.floating = false)
		{

		}
	}*/

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Asleep") {
			if (squish.enabled == true) {
				anim.SetBool ("bloody", true);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag("Radius"))
		{
			{
				transform.parent = null;
				isChild = false;
				anim.SetBool ("floating", false);
			}
		}
	}
}
