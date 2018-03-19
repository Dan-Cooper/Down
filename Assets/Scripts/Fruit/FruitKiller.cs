using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitKiller : MonoBehaviour {

	public Rigidbody2D ball;
	public CircleCollider2D b;
	public Animator anim;
	public float ballSpawnX;
	public float ballSpawnY;
	private bool countdown;
	private float count;

	// Use this for initialization
	void Start () {
		ballSpawnX = transform.position.x;
		ballSpawnY = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (countdown == true) 
		{
			anim.SetBool ("onFire", true);
			anim.SetBool ("vinefruit", false);
			anim.SetBool ("fallingfruit", false);
			anim.SetBool ("onGround", false);
			count++;
			if (count >= 60)
			{
				countdown = false;
				anim.SetBool ("vinefruit", true);
				anim.SetBool ("fallingfruit", false);
				anim.SetBool ("onGround", false);
				anim.SetBool ("onFire", false);
				ball.position = new Vector2 (ballSpawnX, ballSpawnY);
				ball.velocity = new Vector2 (0, 0);
				b.isTrigger = true;
				ball.isKinematic = true;
				count = 0;
			}
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag ("FruitLimit")) 
		{
			countdown = true;
		}
	}
}
