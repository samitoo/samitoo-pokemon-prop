using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScrollingBattleText : MonoBehaviour {

    public float textSpeed;
    public Text textBox;
    public bool isTyping = false;
    bool cancelTyping = false;
    private TurnBasedCombat TBC;
    private BattleSceneSetup BSSetup;

	// Use this for initialization
	void Start () {
        TBC = FindObjectOfType<TurnBasedCombat>();
        BSSetup = FindObjectOfType<BattleSceneSetup>();
	}
	
	// Update is called once per frame
	void Update () {

        if (TBC.currentState == TurnBasedCombat.BattleStates.START
            || TBC.currentState == TurnBasedCombat.BattleStates.ENEMYCHOICE
            || TBC.currentState == TurnBasedCombat.BattleStates.INCOMBAT)
        {
            if (Input.GetButtonDown("Submit"))
            {
                if (isTyping && !cancelTyping)
                {
                    cancelTyping = true;
                }
                //Action calls first and will close the menu if open (by toggling a state w/o menu negating selection
                else if (!isTyping)
                {
                    //hide text box
                    TBC.currentState = TurnBasedCombat.BattleStates.PLAYERCHOICE;
                    Debug.Log("ME");
                }
            }
            else
                return;
        }
	
	}

    public void setText(string text)
    {
        //textBox.text = text;
        StartCoroutine(textScroll(text));
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
