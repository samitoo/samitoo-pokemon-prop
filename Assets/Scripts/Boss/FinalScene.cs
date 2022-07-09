using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour {

    public Animator samiAnim;
    public Text textBox;
    public float textSpeed;
    private bool isTyping = false, cancelTyping = false, nextLine = false, questionAsked = false;
    public bool startFinal = false;
    public MovieTexture successMovie;
    public RawImage movieImageGameobject;
    private AudioSource audio;
    public AudioClip finalTheme;

	// Use this for initialization
	void Start () {
        movieImageGameobject.enabled = false;
        audio = FindObjectOfType<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (startFinal)
        {
            audio.Stop();
            audio.clip = finalTheme;
            audio.Play();
            StartCoroutine(StepOne());
            startFinal = false;
        }
        if (Input.GetButtonDown("Submit"))
        {
            if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
            //State check to load next line
            else if (!isTyping && !cancelTyping)
            {
                nextLine = true;
            }
        }

        if (questionAsked)
        {
            if (Input.GetKeyDown(KeyCode.Y))
                herAnswer(true);
            else if (Input.GetKeyDown(KeyCode.N))
                herAnswer(false);
        }
	
	}

    private IEnumerator StepOne()
    {
        samiAnim.SetTrigger("bounceBall");
        setText("I can't believe that you beat me!");
        while(!nextLine)
       {
        yield return new WaitForSeconds(2);
       }
        nextLine = false;
        StartCoroutine(StepTwo());

    }

    private IEnumerator StepTwo() 
    {
        samiAnim.SetTrigger("ballForward");
        setText("I still have my greatest catch to make");
        while (!nextLine)
        {
            yield return new WaitForSeconds(2);
        }
        nextLine = false;
        StartCoroutine(StepThree());
    }

    private IEnumerator StepThree()
    {
        samiAnim.SetTrigger("point");
        setText("something even greater than the rarest legendary pokemon!");
        while (!nextLine)
        {
            yield return new WaitForSeconds(2);
        }
        nextLine = false;
        StartCoroutine(StepFour());
    }

    private IEnumerator StepFour()
    {
        samiAnim.SetTrigger("handUp");
        setText("I choose you Tasha, will you marry me?");
        while (!nextLine)
        {
            yield return new WaitForSeconds(2);
        }
        nextLine = false;
        questionAsked = true;
        //TODO:  
        //POPUP YES NO BOX
    }

    private void herAnswer(bool yes)
    {
        if (yes)
        {
            playMovie();
        }
        else
        {
            setText(":( Are you sure?");
        }
    }
    public void setText(string text)
    {
        //textBox.text = text;
        StartCoroutine(textScroll(text));
    }

    public void playMovie()
    {
        movieImageGameobject.enabled = true;
        movieImageGameobject.GetComponent<RawImage>().texture = successMovie as MovieTexture;
        successMovie.Play();
        successMovie.loop = true;
    }

    //To scroll through text 
    private IEnumerator textScroll(string lineOfText)
    {
        int letter = 0;
        textBox.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            textBox.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(textSpeed);
        }
        //user cancelled typing, so it fills it with the whole line
        textBox.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }
}
