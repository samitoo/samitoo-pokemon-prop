using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTurnActions : MonoBehaviour {

    public enum playerChoices
    {
        FIGHT,
        POKEMON,
        BAG,
        RUN
    }

    private BattleSceneSetup BSSetup;
    private playerChoices currentChoice;
    private TurnBasedCombat TBC;
    private Fight fight;
    public GameObject Fight, Pokemon, Bag, Run;
    public List<GameObject> buttons;
    public Vector2 movementVector;
    private bool selectThis = false, changedState = false;
    ScrollingBattleText SCB;

	// Use this for initialization
	void Start () {
        currentChoice = playerChoices.FIGHT;
        buttons = new List<GameObject>();
        TBC = FindObjectOfType<TurnBasedCombat>();
        fight = FindObjectOfType<Fight>();
        BSSetup = FindObjectOfType<BattleSceneSetup>();
        SCB = FindObjectOfType<ScrollingBattleText>();
        LoadList();
	}
	
	// Update is called once per frame
	void Update () {

        movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(movementVector != Vector2.zero && TBC.currentState == TurnBasedCombat.BattleStates.PLAYERCHOICE)
        {
            PlayerChangedChoiceTo();
        }

        else if (Input.GetButtonDown("Submit") && TBC.currentState != TurnBasedCombat.BattleStates.MENU)
        {
            Debug.Log("submit after return");
            selectThis = true;
            PlayerChangedChoiceTo();
            
        }
	}


    private void PlayerChangedChoiceTo()
    {
        switch (currentChoice)
        {
            case (playerChoices.FIGHT):
                DisableAllBut(Fight);
                if (selectThis){
                    TBC.currentState = TurnBasedCombat.BattleStates.INCOMBAT;
                    fight.PlayerAttack();
                }
                if (movementVector.x == 1)
                    currentChoice = playerChoices.BAG;
                if (movementVector.y == -1)
                    currentChoice = playerChoices.POKEMON;
                break;
            case (playerChoices.POKEMON):
                DisableAllBut(Pokemon);
                if (selectThis)
                    TBC.currentState = TurnBasedCombat.BattleStates.MENU;
                if (movementVector.x == 1)
                    currentChoice = playerChoices.RUN;
                if (movementVector.y == 1)
                    currentChoice = playerChoices.FIGHT;
                break;
            case (playerChoices.BAG):
                DisableAllBut(Bag);
                if (selectThis)
                {
                    TBC.currentState = TurnBasedCombat.BattleStates.START;
                    SCB.setText("Tasha doesn't carry a backpack!");
                }
                if (movementVector.x == -1)
                    currentChoice = playerChoices.FIGHT;
                if (movementVector.y == -1)
                    currentChoice = playerChoices.RUN;
                break;
            case (playerChoices.RUN):
                DisableAllBut(Run);
                if (selectThis)
                    TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBAT;
                if (movementVector.x == -1)
                    currentChoice = playerChoices.POKEMON;
                if (movementVector.y == 1)
                    currentChoice = playerChoices.BAG;
                break;
        }

        selectThis = false;
    }

    void DisableAllBut(GameObject choice)
    {
        buttons.Remove(choice);
        choice.SetActive(true);
        foreach(GameObject button in buttons){
            button.SetActive(false);
        }
        LoadList();
    }

    void LoadList()
    {
        if (buttons.Count != 0)
            buttons.Clear();
        buttons.Add(Fight);
        buttons.Add(Pokemon);
        buttons.Add(Bag);
        buttons.Add(Run);
    }

    public void SetPlayerChoice(playerChoices choice)
    {
        currentChoice = choice;
        PlayerChangedChoiceTo();
    }
}
