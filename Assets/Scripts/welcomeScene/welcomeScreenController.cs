using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class welcomeScreenController : MonoBehaviour {

    public enum WelcomeStates
    {
        OAKINTRO,
        WELCOMETOPOKEMONWORLD,
        INTRODUCINGTASHA,
        INTRODUCINGRIVAL,
        SHRINK
    }

    public WelcomeStates currentState;
    public GameObject oak,showPokemon, pokeball, player, rival;
    private TextBoxManager textboxManager;
    public Animator tashaAnim;
    public bool changedState = true;
    private bool lineChanged = false;

	// Use this for initialization
	void Start () {
        textboxManager = GetComponent<TextBoxManager>();
        currentState = WelcomeStates.SHRINK;//set to somethign so it doesnt match the method call
        changeState(WelcomeStates.OAKINTRO);
        tashaAnim.StopPlayback();
	}
	
	// Update is called once per frame
	void Update () {
        checkLine();
	}

    private void checkLine()
    {
        if (textboxManager.currentLine == 5)
        {
            changedState = true;
            changeState(WelcomeStates.WELCOMETOPOKEMONWORLD);
        }
        else if (textboxManager.currentLine == 8)
        {
            changedState = true;
            changeState(WelcomeStates.OAKINTRO);
        }
        else if (textboxManager.currentLine == 9)
        {
            changedState = true;
            changeState(WelcomeStates.INTRODUCINGTASHA);
        }
        else if (textboxManager.currentLine == 13)
        {
            changedState = true;
            changeState(WelcomeStates.INTRODUCINGRIVAL);
        }
        else if (textboxManager.currentLine == 18)
        {
            changedState = true;
            changeState(WelcomeStates.INTRODUCINGTASHA);
        }
        else if (textboxManager.currentLine == 20)
        {
            changedState = true;
            changeState(WelcomeStates.SHRINK);
            //playanimation
            //tashaAnim.StartPlayback();
            
        }
        else if (textboxManager.currentLine >= 21)
        {
            Application.LoadLevel(2);
        }

    }

    private void changeState(WelcomeStates passedState)
    {
        if (changedState && passedState != currentState)
        {
            switch (passedState)
            {
                case (WelcomeStates.OAKINTRO):
                    currentState = WelcomeStates.OAKINTRO;
                    oak.SetActive(true);
                    showPokemon.SetActive(false);
                    pokeball.SetActive(false);
                    //player.SetActive(false);
                    player.GetComponent<Image>().enabled = false;
                    rival.SetActive(false);
                    break;
                case (WelcomeStates.WELCOMETOPOKEMONWORLD):
                    currentState = WelcomeStates.WELCOMETOPOKEMONWORLD;
                    oak.SetActive(true);
                    showPokemon.SetActive(true);
                    pokeball.SetActive(true);
                    //player.SetActive(false);
                    player.GetComponent<Image>().enabled = false;
                    rival.SetActive(false);
                    break;
                case (WelcomeStates.INTRODUCINGTASHA):
                    currentState = WelcomeStates.INTRODUCINGTASHA;
                    tashaAnim.Stop();
                    oak.SetActive(false);
                    showPokemon.SetActive(false);
                    pokeball.SetActive(false);
                    //player.SetActive(true);
                    player.GetComponent<Image>().enabled = true;
                    tashaAnim.Rebind();
                    rival.SetActive(false);
                    break;
                case (WelcomeStates.INTRODUCINGRIVAL):
                    currentState = WelcomeStates.INTRODUCINGRIVAL;
                    oak.SetActive(false);
                    showPokemon.SetActive(false);
                    pokeball.SetActive(false);
                    //player.SetActive(false);
                    player.GetComponent<Image>().enabled = false;
                    rival.SetActive(true);
                    break;
                case (WelcomeStates.SHRINK):
                    currentState = WelcomeStates.SHRINK;
                    oak.SetActive(false);
                    showPokemon.SetActive(false);
                    pokeball.SetActive(false);
                    //player.SetActive(false);
                    player.GetComponent<Image>().enabled = true;
                    tashaAnim.Rebind();
                    rival.SetActive(false);
                    //tashaAnim.Play("tashaShrink", -1, 0.0f);
                    tashaAnim.SetTrigger("shrink");
                    Debug.Log("shrink called");
                    break;
            }
            changedState = false;
        }
    }
}
