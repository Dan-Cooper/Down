using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour
{

	public bool IsLeft;
	public float jumpforce;
	public Rigidbody2D player;

	private bool triggered;

	private void Start()
	{
		triggered = false;
	}

	private void Update()
	{
		if(triggered && IsLeft)
			player.AddForce(new Vector2(jumpforce, 0), ForceMode2D.Impulse);
		if(triggered && !IsLeft)
			player.AddForce(new Vector2(-jumpforce, 0), ForceMode2D.Impulse);
	}



	private void OnTriggerEnter2D(Collider2D other)
	{
		if (Input.GetKeyDown(KeyCode.W) && (other.tag == "Wall" || other.tag == "Platform"))
		{
			triggered = true;
			Debug.Log("on");
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if ((other.tag == "Wall" || other.tag == "Platform"))
		{
			triggered = false;
			Debug.Log("off");
		}
	}
}