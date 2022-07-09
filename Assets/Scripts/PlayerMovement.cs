using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rbody;
    private Animator anim;
    public bool canMove, isMoving, visitedOaksLab = false;
    Vector2 moveDirection = Vector2.zero;

    public enum PlayerZone
    {
        HEROHOME,
        PALLETTOWN,
        OAKSLAB,
        ROUTE1,
        VIRIDIANCITY,
        POKECENTER,
        GYM
    }

    public PlayerZone currenZone; 
	// Use this for initialization
	void Start () {

        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        //Stops movement
        if (!canMove)
        {
            anim.SetBool("isWalking", false);
            return;
        }
        
        //GetAxis goes floats values, GetAxisRaw is only 0 or 1
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = Vector2.zero;

        if (movementVector.x > 0) {
            MoveDirection(true, 1);
        }
        else if (movementVector.x < 0){
            MoveDirection(true, -1);
        }
        else if (movementVector.y > 0){
            MoveDirection(false, 1);
        }
        else if (movementVector.y < 0){
            MoveDirection(false, -1);
        }
        else{
            anim.SetBool("isWalking", false);
            isMoving = false;
        }
	
	}
    private void MoveDirection(bool x, int direction)
    {
        if (x)
            moveDirection.x = direction;
        else
            moveDirection.y = direction;
        anim.SetBool("isWalking", true);
        anim.SetFloat("input_x", moveDirection.x);
        anim.SetFloat("input_y", moveDirection.y);
        isMoving = true;
        rbody.MovePosition(rbody.position + moveDirection * Time.deltaTime);
    }

}
