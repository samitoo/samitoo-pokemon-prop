using UnityEngine;
using System.Collections;

public class samiRivalBehaviors : MonoBehaviour {

    Animator anim;
    GameObject player;
    TextBoxManager theTextBox;
    public GameObject walkToPoint, walktoOutside;
    public bool tashaPassedBy = false, canStartMoving = false, leave = false;
    public float speed;
    float step;
    public bool istyping;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        theTextBox = FindObjectOfType<TextBoxManager>();
        gameObject.GetComponent<ActivateTextAtLine>().skipActivation = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        istyping = theTextBox.isTyping;
        step = speed * Time.deltaTime;
        if (canStartMoving && !leave)
        {
            MoveToThisPoint(walkToPoint.transform);
        }
        else if (leave)
        {
            MoveToThisPoint(walktoOutside.transform);
        }
            
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && tashaPassedBy)
        {
            //Text starts from ActivateAtLineScript
            player.GetComponent<PlayerMovement>().canMove = false;
            canStartMoving = true;
            StartCoroutine(waitForTextToFinish());
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            tashaPassedBy = true;
            gameObject.GetComponent<ActivateTextAtLine>().skipActivation = false;
        }
    }

    private void MoveToThisPoint(Transform newPoint)
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newPoint.transform.position, step);
        if (newPoint == walkToPoint)
        {
            anim.SetFloat("input_x", 1.0f);
        }
        else if (newPoint == walktoOutside)
        {
            anim.SetFloat("input_y", -1.0f);
        }
        else
        {
            anim.SetFloat("input_x", 0.0f);
            anim.SetFloat("input_y", 0.0f);
        }
    }

    private IEnumerator waitForTextToFinish()
    {
        while (!player.GetComponent<PlayerMovement>().canMove)
        {
            yield return new WaitForSeconds(1f);
        }
            leave = true;
            Debug.Log("leave changed!");
        
        
    }
}
