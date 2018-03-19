using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TitleLoad : MonoBehaviour {

	public string sceneName;
	public GameObject txt1;
	public GameObject txt2;
	public GameObject txt3;
	public GameObject txt4;

	private float count = 0;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			SceneManager.LoadScene(sceneName);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void FixedUpdate ()
	{
		if (count < 300)
		{
			count++;
	}
		if (count == 300)
		{
			txt1.SetActive(true);
			txt2.SetActive(true);
			txt3.SetActive(true);
			txt4.SetActive(true);
		}
	}
}
