using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{

	private float useSpeed;
	public float directionSpeed = 3.0f;
	float origX;
	public float distance = 5.0f;
	public GameObject plat;

	// Use this for initialization
	void Start () {
		origX = plat.GetComponent<Rigidbody2D>().position.x;
		useSpeed = -directionSpeed;
	}

	// Update is called once per frame
	void Update () {
		if(origX - plat.GetComponent<Rigidbody2D>().position.x > distance)
		{
			useSpeed = directionSpeed; //flip direction
		}
		else if(origX - plat.GetComponent<Rigidbody2D>().position.x < -distance)
		{
			useSpeed = -directionSpeed; //flip direction
		}
		transform.Translate(useSpeed*Time.deltaTime,0,0);
	}
}

