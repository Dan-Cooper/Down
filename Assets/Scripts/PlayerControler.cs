using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine.Assertions.Comparers;
using UnityEngine.SceneManagement;

public class PlayerControler : MonoBehaviour
{

	/*LIST OF ANIMATION TRIGGERS NEEDED:
	jumping/falling (current airborne doesn't work; possibly detect no floor collision? jumping is when upward motion present, falling is when no upward motion)
	climbing/sliding (climbing when player is sticking on wall, sliding is when they slide down) [currently have falling]
	attacking (implement when you have attacking figured out)
	damaged (ditto)
	dragging/pushing (when you are interacting with a block. depends on orientation of the block, not sure how you'd do this.)
	death (self explanatory)
*/
    public bool debug;
    public float moveSpeed = 10f;
	public int playerHealth = 3;
	public PlayerControler prntrb2d; //using this to grab the rigidbody from the parent, since having rigid and animator on the same object breaks movement
	public GameObject fullHeart1;
	public GameObject fullHeart2;
	public GameObject fullHeart3;
	public GameObject emptyHeart1;
	public GameObject emptyHeart2;
	public GameObject emptyHeart3;
	public KeyCode jumpkey = KeyCode.W;
    public float jumpStrength = 10;
	public float invulnFrames = 30f;
	public float attackCooldown = 30f;
	public bool Alive = true;
	public bool canInteractBox = false;
	public string sceneName;

	private bool GameOver = false;
	private float deathCount = 0f;
	private float attackCount = 0f;
	private float damagedCount = 0f;
	private float fallingCount = 0f;
    private bool facingR = false;
    //private bool facingC = true;
    //private bool jumping = false;

	Animator anim; //FOR ANIMATION
	//BoxCollider2D box2D;

	// Use this for initialization
	void Start ()
	{
	    //box2D = GetComponent<BoxCollider2D>();   
		//box2D.size = new Vector2(1.18f, 2.2f);
		anim = GetComponent<Animator>(); //FOR ANIMATON
		anim.SetBool ("facingCenter", true);
	}

	/*
    *Referenced Unity 2D character controllers tut
    */
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (Alive == true) {
			if (damagedCount > 0) {
				damagedCount--;
			}

			if (attackCount > 0) {
				attackCount--;
			}
			
			float move = Input.GetAxis ("Horizontal");
			//SetFloat("Speed", Mathf.Abs(move));    //FOR ANIMATION - I'm using a different method, keeping the original in case it's better
			prntrb2d.GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * moveSpeed, prntrb2d.GetComponent<Rigidbody2D> ().velocity.y);

			if (move > 0 && !facingR) {
				RLFlip ();
				ClearBools ();
				anim.SetBool ("facingRight", true);
			} else if (move < 0 && facingR) {
				RLFlip ();
				ClearBools ();
				anim.SetBool ("facingLeft", true);
			}

			if (move != 0) {
				anim.SetBool ("idle", false);
				anim.SetBool ("running", true);
			} else if (move == 0) {
				anim.SetBool ("running", false);
				StopShortTrigger ();
			}
	

			if (prntrb2d.GetComponent<Rigidbody2D> ().velocity.y < -1) {
				fallingCount++;
				if (fallingCount > 15) {
					anim.SetTrigger ("JumptoFall");
					anim.SetBool ("falling", true);
					//Debug.Log ("falling");
				}
			}
			if (prntrb2d.GetComponent<Rigidbody2D> ().velocity.y > -1) {
				fallingCount = 0f;
				//anim.SetBool ("jumping", true);
				anim.SetBool ("falling", false);
				/*else if (move != 0  && IsAirborn()) 
		{
			anim.SetBool ("idle", false);
			anim.SetBool ("running", false);
			anim.SetBool ("jumping", true);

		}*/
			}

			if (Input.GetKeyDown ("space")) {
				Attacking ();
			}
		// health/ UI
		}
			if (playerHealth == 3) 
			{
				fullHeart1.SetActive(true);
				fullHeart2.SetActive(true);
				fullHeart3.SetActive(true);
				emptyHeart1.SetActive (false);
				emptyHeart2.SetActive (false);
				emptyHeart3.SetActive (false);
			}
			if (playerHealth == 2) 
			{
				fullHeart1.SetActive(true);
				fullHeart2.SetActive(true);
				fullHeart3.SetActive(false);
				emptyHeart1.SetActive (false);
				emptyHeart2.SetActive (false);
				emptyHeart3.SetActive (true);
			}
			if (playerHealth == 1) 
			{
				fullHeart1.SetActive(true);
				fullHeart2.SetActive(false);
				fullHeart3.SetActive(false);
				emptyHeart1.SetActive (false);
				emptyHeart2.SetActive (true);
				emptyHeart3.SetActive (true);
			}
		if (GameOver) 
		{
			if (deathCount < 180) {
				deathCount++;
			}
			if (deathCount >= 180) {
				SceneManager.LoadScene(sceneName);
			}
		}
	}

	void Attacking()
	{
		if (attackCount <= 0)
		{
			attackCount = attackCooldown;
			anim.SetTrigger ("attacking");
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag("Ground"))
		{
			anim.SetTrigger ("StopFalling");
			anim.SetBool ("falling", false);
			anim.SetBool ("jumping", false);
		}
		if (other.CompareTag("Box"))
		{
			canInteractBox = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy") 
		{
			Damaged ();
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag("Ground"))
		{
			anim.SetTrigger ("StartJumping");
			anim.SetBool ("falling", true);
			anim.SetBool ("jumping", true);
		}
		if (other.CompareTag("Box"))
		{
			canInteractBox = false;
		}
	}

	void StopShortTrigger ()
	{
		if (anim.GetBool("idle") == false) {
			anim.SetTrigger ("StopRunning");
			anim.SetBool ("idle", true);
		} else {
			return;
		}
	}

	void Damaged()
	{
		if (damagedCount <= 0)
		{
			if (playerHealth == 1) {
				Death ();
			} else {
				damagedCount = invulnFrames;
				anim.SetTrigger ("damaged");
				playerHealth--;
			}
		}
	}

	void Death()
	{
		Alive = false;
		anim.SetBool ("running", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("falling", false);
		anim.SetBool ("jumping", false);
		GetComponent<BoxCollider2D>().enabled = false;
		prntrb2d.GetComponent<Rigidbody2D> ().isKinematic = true;
		prntrb2d.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		anim.SetTrigger ("die");
		GameOver = true;
	}


    private void Update()
    {
        if (debug)
        {
			Debug.Log(prntrb2d.GetComponent<Rigidbody2D>().velocity.y.ToString());
        }

		if (Input.GetKeyDown(jumpkey) && IsAirborn())
		{
			prntrb2d.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,jumpStrength), ForceMode2D.Impulse);
		}

		if (Input.GetKeyDown("a"))
			{
				anim.SetBool ("facingLeft", true);
				anim.SetBool ("facingRight", false);
			}
		if (Input.GetKeyDown("d"))
			{
				anim.SetBool ("facingRight", true);
				anim.SetBool ("facingLeft", false);
			}
    }

    /**
     *Used for animation fliping the sprite left and right.
     */
    void RLFlip()
    {
        facingR = !facingR;
        //Vector3 size = transform.localScale;
        //size.x *= 1;
        //transform.localScale = size;
    }
		

    /**
    *DO NOT USE
    *Test method for more jagged motion.
    */
    float DigiMove(float move)
    {
        int x = (int) move;


        return x;
    }

    /**
    *Controles Jumping. Tests if player velocity is >< = 1
    */
    bool IsAirborn()
    {
		if((prntrb2d.GetComponent<Rigidbody2D>().velocity.y >= 1f || prntrb2d.GetComponent<Rigidbody2D>().velocity.y <= -1f) ) return false;

        return true;
    }


	//Used to shut off all bools whenever an animation is triggered. There's almost certainly a better way to do this.
	void ClearBools()
	{
		anim.SetBool ("facingCenter", false);
		anim.SetBool ("facingLeft", false);
		anim.SetBool ("facingRight", false);
		anim.SetBool ("running", false);
		anim.SetBool ("idle", false);
		anim.SetBool ("falling", false);
		anim.SetBool ("jumping", false);
	}

}
