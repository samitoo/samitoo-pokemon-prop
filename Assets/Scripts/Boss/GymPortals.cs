using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class GymPortals : MonoBehaviour {

    private GameObject player;
    public Transform  moveTo;
    private Transform resetPos;
    public float speed;
    float step;
    bool moveMe = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        resetPos = GameObject.Find("ResetPOS").transform;
	}
	
	// Update is called once per frame
	void Update () {

        step = speed * Time.deltaTime;
        if (moveMe)
            MoveToThisPoint(moveTo);
        if(Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = resetPos.position;
            moveMe = false;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
            moveMe = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //if(other.gameObject == player)
        //    moveMe = false;
    }
    private void MoveToThisPoint(Transform moveTo)
    {
        player.transform.position = Vector2.MoveTowards(player.transform.position, moveTo.position, step);
        if (player.transform.position == moveTo.position)
            moveMe = false;
    }
}
