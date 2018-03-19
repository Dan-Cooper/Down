using UnityEngine;
using System.Collections;

public class DoorControler : MonoBehaviour {

	public GameObject[] badDudes;
	public DoorControler otherdoor;
	public bool open = false;
	public ButtonControl butn;
	Animator anim;

	private int j = 0;
	private int i = 0;
	//private buttons b;


	void Start () {
		anim = GetComponent<Animator>();
	}
		
	void Update () 
	{
		if (butn.buttons [i].IsTouching (butn.player) || butn.buttons [i].IsTouching (butn.ball)) {
			Debug.Log ("triggered");
			open = true;
			anim.SetBool ("OpenRedDoor", true);
			anim.SetTrigger ("TriggerRedDoor");
		}
		if (butn.Pressed == false) {
			open = false;
			anim.SetBool ("OpenRedDoor", false);
			anim.SetTrigger ("TriggerRedDoor");
		}
		if (badDudes[j].GetComponent<BadDudeControler>().dead && !otherdoor.open)
		{
			open = true;
			anim.SetBool ("OpenBlueDoor", true);
			anim.SetTrigger ("TriggerBlueDoor");
			j++;
		}
		if (j >= badDudes.Length-1)
			j = 0;
	}


}
