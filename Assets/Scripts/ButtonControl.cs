using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {

	public BoxCollider2D[] buttons;
	public bool and;
	public bool or;
	public bool not;

	public BoxCollider2D player;
	public CircleCollider2D ball;
	public BoxCollider2D door;
	public Animator button;
	public bool Pressed = false;

	private Queue bProssess;
	private int i = 0;
	//private int delay = 0;


	// Use this for initialization
	void Start () {
		//button = GetComponent<Animator>();
		button.SetBool ("InertButton", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (buttons [i].IsTouching (player) || buttons [i].IsTouching (ball)) {
			Debug.Log ("pressed");
			button.SetBool ("ActiveButton", true);
			Pressed = true;
			/*if (delay < 15) {
				delay++;
			} else if (delay >= 15) {
				ClearBool();
				anim.SetBool ("ActiveButton", true);
			}*/
		} else {
			ClearBool ();
			Pressed = false;
		}
		i++;
		if(i> buttons.Length-1) i=0;
	}

	/*bool GetState(int i){

	}*/


	void ClearBool()
	{
		button.SetBool ("InertButton", false);
		button.SetBool ("ActiveButton", false);
	}
}
