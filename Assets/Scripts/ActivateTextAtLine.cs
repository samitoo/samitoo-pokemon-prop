using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;
    public int startLine, endLine;
    public TextBoxManager theTextBox;
    public bool destroyWhenCompleted, requireButtonPress, secondDialogPress, skipActivation = false;
    bool waitForPress;
    private int colliderCount = 0;
    private GameObject player;

	// Use this for initialization
	void Start () {
        theTextBox = FindObjectOfType<TextBoxManager>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {


        if (waitForPress && Input.GetButtonDown("Submit") && !theTextBox.isTyping)
        {
            sendAndStartText();
            waitForPress = false;
        }
        else
        {
            return;
        }
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (skipActivation)
        {
            Debug.Log("skipped!");
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }

            sendAndStartText();
        }
    }

 
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            waitForPress = false;
        }
    }

    void sendAndStartText()
    {
        theTextBox.LoadScript(theText);
        theTextBox.currentLine = startLine;
        theTextBox.endAtLine = endLine;
        theTextBox.EnableTextBox();


        if (destroyWhenCompleted)
        {
           // Destroy(gameObject);
            StartCoroutine(waitForTextToDestroy());
        }
    }

    private IEnumerator waitForTextToDestroy()
    {
        while (theTextBox.isTyping)
        {
            yield return new WaitForSeconds(2);
        }
        BoxCollider2D[] colliders = gameObject.GetComponents<BoxCollider2D>();
        colliders[colliderCount].enabled = false;
        //colliderCount++;
        if (secondDialogPress)
        {
            requireButtonPress = true;
            destroyWhenCompleted = false; //already destroyed
        }

    }
}
