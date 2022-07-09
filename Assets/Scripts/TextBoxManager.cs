using UnityEngine;
using System.Collections;
using UnityEngine.UI; //UI elements new in 5

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;
    public Text theText;
    public TextAsset textFile;
    public string[] textLines;
    public int currentLine, endAtLine;
    public bool isActive, stopPlayerMovement;
    public bool isTyping = false;
    bool cancelTyping = false;
    public float textSpeed;

    //Add playerController to stop movement
    public PlayerMovement player;

	// Use this for initialization
	void Start () {

        if(textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0)
            endAtLine = textLines.Length - 1;

        if (isActive)
            EnableTextBox();
        else
            DisableTextBox();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!isActive)
        {
            return;
        }

        //theText.text = textLines[currentLine];

        if (Input.GetButtonDown("Submit"))
        {
            if (!isTyping)
            {
                currentLine += 1;
                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(textScroll(textLines[currentLine]));
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
        //Avoid potential double press of Submit
        else
        {
            return;
        }

	}

    //To scroll through text 
    private IEnumerator textScroll(string lineOfText)
    {
        int letter = 0; 
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter+=1;
            yield return new WaitForSeconds(textSpeed);
        }
        //user cancelled typing, so it fills it with the whole line
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }


    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;
        if(player != null)
            player.canMove = false;

        StartCoroutine(textScroll(textLines[currentLine]));
    }

    //Always want the player to be able to move after disable either way
    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
        if (player != null)
            player.canMove = true;
    }

    public void LoadScript(TextAsset theText)
    {
        if (theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }

    }

}
