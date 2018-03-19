using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public CircleCollider2D b;
	public BoxCollider2D player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (b.IsTouching (player))
			b.isTrigger = true;
		else
			b.isTrigger = false;
	}
}
