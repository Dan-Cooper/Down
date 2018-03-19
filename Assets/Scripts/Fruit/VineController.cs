using UnityEngine;
using System.Collections;

public class VineController : MonoBehaviour {

	public BoxCollider2D player;
	public Rigidbody2D ball;
	public CircleCollider2D b;
	public BoxCollider2D vine;
	public KeyCode cut;

	public bool debug;
	int h;

	// Use this for initialization
	void Start () {
		h = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (vine.IsTouching(player) && Input.GetKeyDown(cut)) {
			ball.WakeUp();
			b.isTrigger = false;
			ball.isKinematic = false;
		}

		if (debug && h < 30) {
			Debug.Log (ball.IsAwake ());
			if (vine.IsTouching (player))
				Debug.Log ("Touching");
			h++;
		} else
			h = 0;
	}



}
