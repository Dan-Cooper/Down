using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadDudeHealth : MonoBehaviour {

	public BadDudeControler badDude;

	void Update ()
	{
		if (badDude.isAsleep == true) 
		{
			gameObject.tag = "Asleep";
		}

		if (badDude.isAsleep == false) 
		{
			gameObject.tag = "Enemy";
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag("Attack") && !badDude.dead)
		{
			badDude.Injured();
		}

		if (other.CompareTag("Squish") && !badDude.dead)
		{
			badDude.Die();
		}
	}
}
