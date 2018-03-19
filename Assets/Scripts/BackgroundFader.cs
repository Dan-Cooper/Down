using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFader : MonoBehaviour {

	SpriteRenderer srend;

	void Start () {
		srend= GetComponent<SpriteRenderer>();
		srend.color = new Color (1f, 1f, 1f, .5f);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
