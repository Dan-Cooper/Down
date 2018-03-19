using UnityEngine;
using System.Collections;

public class BadDudeControler : MonoBehaviour
{

    public float maxTime;
    public float speed;
    public float damage;
    public float health;

	public GameObject player;
	public GameObject[] BadDudes;

    private Rigidbody2D playerRB;
    public Rigidbody2D badDude;

	public BadDudeAnimator badanim;
    public BoxCollider2D combat;
    private BoxCollider2D playerBC;
	public CircleCollider2D badCC;

	public bool isAttacked;
	public bool isNear;
	public bool isDefend;

    public bool isAsleep = true;
	public bool dead = false;
	private bool animDone = false;

    public float maxHelth;
    private float timer = 0;
	public float wakeup = 0;
	private bool facingR = true;

	public bool isSlowed;
	private int i = 0;

	void Start ()
	{
		maxHelth = health;
		playerRB = player.GetComponent<Rigidbody2D>();
		playerBC = player.GetComponentInChildren<BoxCollider2D>();
		badanim.anim.SetBool ("facingLeft", true);
		badanim.anim.SetBool ("facingRight", false);
	}

	void Update () {

		if (!dead && !isAsleep) {
			badDude.transform.position = Vector2.MoveTowards (badDude.position, playerRB.position, speed * Time.deltaTime);
		}


		if (isAttacked) {
			if (health < maxHelth) 
			{
				if(animDone && !dead){
				isAsleep = false;
				badDude.isKinematic = false;
				badCC.isTrigger = false;
				badanim.anim.SetTrigger ("awake");
				}

				if (wakeup >= 0 && wakeup < 60 &&!animDone) {
					wakeup++;
					//Debug.Log ("wakeup+");
				}

				if (wakeup >= 60 && !animDone) {
					badanim.anim.SetBool ("moving", true);
					//Debug.Log ("wakeup60");
					animDone = true;
				}
				Attack ();
			}

			if (isNear) 
			{
				if (combat.IsTouching (playerBC) &&!dead) 
				{
					isAsleep = false;
					badDude.isKinematic = false;
					badCC.isTrigger = false;
					badanim.anim.SetTrigger ("moving");

					if (!isSlowed) { //Slows the player by half once?
						player.GetComponent<PlayerControler>().moveSpeed /= 2;
						isSlowed = true;
						//Debug.Log ("BOOOOOOOOOOP");
					}
					//Debug.Log ("Boop");
				} 
				else if (isSlowed && !combat.IsTouching (playerBC)) { //Returns speed to normal
					player.GetComponent<PlayerControler> ().moveSpeed *= 2;
					isSlowed = false;
					//Debug.Log ("No Boop");
				}
				Attack ();
			}

			if (isDefend) {
				if (BadDudes != null && !BadDudes [i].GetComponent<BadDudeControler> ().isAsleep) {
					isAsleep = false;
					badDude.isKinematic = false;
					badCC.isTrigger = false;
					i++;
				}
				if (BadDudes.Length > i)
					i = 0;
				Attack ();
			}

			if (health <= 0) {
				Die ();
			}
		}
	}

	void FixedUpdate()
	{
		if (badDude.transform.position.x > playerRB.transform.position.x && !facingR)
		{
			Debug.Log ("flippin");
			RLFlip();
			badanim.anim.SetBool ("facingRight", false);
			badanim.anim.SetBool ("facingLeft", true);
		}
		else if (badDude.transform.position.x < playerRB.transform.position.x && facingR)
		{
			RLFlip();
			badanim.anim.SetBool ("facingLeft", false);
			badanim.anim.SetBool ("facingRight", true);
		}
	}

	void RLFlip()
	{
		facingR = !facingR;
	}

	public void Die()
	{
		if (dead == false) 
		{
			Debug.Log ("imdead");
			badanim.anim.SetTrigger ("die");
			badanim.anim.SetBool ("moving", false);
			isAsleep = true;
			dead = true;
			badDude.isKinematic = true;
			badCC.isTrigger = true;

		}
	}

	public void Injured ()
	{
		health--;
		badanim.anim.SetTrigger ("damage");
	}

	void Attack()
	{
		if (combat.IsTouching(playerBC) && timer>=maxTime)
		{
			//PlayerControler.DamagePlayer(damage);
		}
		timer++;
		if (playerBC.IsTouching(combat) && Input.GetButtonDown("Fire1"))
		{
			DamageDude(5);
		}
	}


	public void DamageDude(float _damage)
	{
		health -= _damage;
	}
}
