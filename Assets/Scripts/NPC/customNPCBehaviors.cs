using UnityEngine;
using System.Collections;

public class customNPCBehaviors : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //Chris's jump excitement
        if (other.gameObject == player && gameObject.name == "npcChris")
        {
            Debug.Log("chris found tasha");
            gameObject.GetComponent<Animator>().SetTrigger("chrisJump");
        }

        //Old man blocking the path from palletcity
        if (other.gameObject == player && gameObject.name == "npcOldMan")
        {
            Debug.Log("oldMan found tasha");
            if (player.GetComponent<PlayerMovement>().visitedOaksLab)
            {
                BoxCollider2D[] colliders = gameObject.GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D col in colliders)
                {
                    col.enabled = false;
                }
            }
        }

        //Changing the visited oak flag
        if (other.gameObject == player && gameObject.name == "npcOak")
        {
            Debug.Log("Visited Oaks Lab!");
            player.GetComponent<PlayerMovement>().visitedOaksLab = true;
        }

        if (other.gameObject == player && gameObject.name == "npcBlonde")
        {
            Debug.Log("Blond found tasha!");
            gameObject.GetComponent<Animator>().SetTrigger("wave");
        }

        if (other.gameObject == player && gameObject.name == "angelClose")
        {
            Debug.Log("You blinked!");
            gameObject.GetComponent<Animator>().SetTrigger("dontBlink");
        }
    }

}
