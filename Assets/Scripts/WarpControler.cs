using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WarpControler : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D warp;
    public string sceneName;
    public float xPosition;
    public float yPosition;

    void Update()
    {
        if (warp.IsTouching(player.GetComponent<BoxCollider2D>()))
        {
            Debug.Log("POKE");
           // player.transform.position = new Vector3(xPosition, yPosition, 0);
            SceneManager.LoadScene(sceneName);
            //Application.LoadLevel(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D  other)
    {
        if(other.tag == "Player") {
           // other.transform.position = new Vector3(xPosition, yPosition, 0);
            SceneManager.LoadScene(sceneName);
        }
    }
}
