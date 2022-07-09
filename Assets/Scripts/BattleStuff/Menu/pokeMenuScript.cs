using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pokeMenuScript : MonoBehaviour {

    public enum menuChoices
    {
        SELECTION1,
        SELECTION2,
        SELECTION3,
        SELECTION4,
        SELECTION5,
        BACKBUTTON
    }

    public GameObject backButton;
    public GameObject[] selections, currentSelections;
    public Image[] pokemonImages;
    public Image currentlySelectedPokemon;
    public Image[] hpBars;
    public Image selectedHPBar;
    public Text[] pokemonNames, pokemonLevels, currentHP, totalHP;
    public Text currentPokemonName, currentPokemonLevel;
    public menuChoices currentChoice;
    private Vector2 inputVector, movementVector;
    private TurnBasedCombat TBC;
    public bool selectThis = false;
    private float nextStep;
    public float stepTime;
    private PokedexXML playerPokedex;
    private BattleCanvasSetup BCSetup;
    
	// Use this for initialization
	void Start () {
        currentChoice = menuChoices.SELECTION1;
        TBC = FindObjectOfType<TurnBasedCombat>();
        playerPokedex = GameObject.FindGameObjectWithTag("Player").GetComponent<PokedexXML>();
        BCSetup = FindObjectOfType<BattleCanvasSetup>();
        LoadPokedexOnMenu();
	}
	
	// Update is called once per frame
	void Update () {

        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Submit") && TBC.currentState == TurnBasedCombat.BattleStates.MENU)
        {
            selectThis = true;
        }
        else if (Input.GetButtonDown("BackButton"))
        {
            currentChoice = menuChoices.BACKBUTTON;
        }

        if (Time.time > nextStep)
        {
            nextStep = Time.time + stepTime;
            movementVector = inputVector;
            switchStatementMethod();
        }

        UpdateHPBars();
	
	}

    private void HighlightedSelection(int choice)
    {
        //Random value higher than array length
        if (choice == 10)
            backButton.SetActive(true);
        else
            backButton.SetActive(false);
            
        //loop of both array, change happens on same value
        for (int i = 0; i < currentSelections.Length; i++)
        {
            if (choice == i)
                currentSelections[i].SetActive(true);
            else
                currentSelections[i].SetActive(false);
        }

        for (int j = 0; j < selections.Length; j++)
        {
            if (choice != j)
                selections[j].SetActive(true);
            else
                selections[j].SetActive(false);
        }
    }

    private void UpdateHPBars()
    {
        //selected image is out of the array to avoid OutOfRange Exception 
        selectedHPBar.fillAmount = ((float)playerPokedex.currentPokemon.currentHp / (float)playerPokedex.currentPokemon.hp);
        for (int p = 0; p < hpBars.Length; p++)
        {
            hpBars[p].fillAmount = ((float)playerPokedex.pcXML.Pokemon[p].currentHp / (float)playerPokedex.pcXML.Pokemon[p].hp);
        }

        //update current hp text
        for (int c=0; c<currentHP.Length; c++)
        {
            currentHP[c].text = playerPokedex.pcXML.Pokemon[c].currentHp.ToString();
        }

        //update max hp bars
        for (int hp = 0; hp < totalHP.Length; hp++)
        {
            totalHP[hp].text = playerPokedex.pcXML.Pokemon[hp].hp.ToString();
        }
    }
    private void selectCurrentPokemon(int pokemonIndex)
    {
        Debug.Log("selected current pokemon" + playerPokedex.pcXML.Pokemon[pokemonIndex].name.ToUpper());
        playerPokedex.currentPokemon = playerPokedex.pcXML.Pokemon[pokemonIndex];
        currentlySelectedPokemon.sprite = Resources.Load<Sprite>(playerPokedex.currentPokemon.imageFront);
        currentPokemonName.text = playerPokedex.pcXML.Pokemon[pokemonIndex].name.ToUpper();
        currentPokemonLevel.text = playerPokedex.pcXML.Pokemon[pokemonIndex].level.ToString();
    }

    private void LoadPokedexOnMenu()
    {
        currentlySelectedPokemon.sprite = Resources.Load<Sprite>(playerPokedex.currentPokemon.imageFront);
        currentPokemonName.text = playerPokedex.currentPokemon.name.ToUpper();
        currentPokemonLevel.text = playerPokedex.currentPokemon.level.ToString();

        //Add  HP
        //Load images
        for (int k = 0; k < pokemonImages.Length; k++)
        {
            string imageString = (playerPokedex.pcXML.Pokemon[k].imageFront);
            //Debug.Log(imageString);
            pokemonImages[k].sprite = Resources.Load<Sprite>(imageString);
        }

        //Load Names
        for (int L=0; L<pokemonNames.Length;L++)
        {
            string name = (playerPokedex.pcXML.Pokemon[L].name).ToUpper();
            pokemonNames[L].text = name;
        }

        //Load levels
        for (int m = 0; m < pokemonLevels.Length; m++)
        {
            string level = (playerPokedex.pcXML.Pokemon[m].level).ToString();
            pokemonLevels[m].text = level;
        }
    }

    private void switchStatementMethod()
    {
        switch (currentChoice)
        {
            case (menuChoices.SELECTION1):
                HighlightedSelection(0);
                if (selectThis)
                    selectCurrentPokemon(0);
                if (movementVector.y == 1)
                    currentChoice = menuChoices.BACKBUTTON;
                if (movementVector.y == -1)
                    currentChoice = menuChoices.SELECTION2;
                break;
            case (menuChoices.SELECTION2):
                HighlightedSelection(1);
                if (selectThis)
                    selectCurrentPokemon(1);
                if (movementVector.y == 1)
                    currentChoice = menuChoices.SELECTION1;
                if (movementVector.y == -1)
                    currentChoice = menuChoices.SELECTION3;
                break;
            case (menuChoices.SELECTION3):
                HighlightedSelection(2);
                if (selectThis)
                    selectCurrentPokemon(2);
                if (movementVector.y == 1)
                    currentChoice = menuChoices.SELECTION2;
                if (movementVector.y == -1)
                    currentChoice = menuChoices.SELECTION4;
                break;
            case (menuChoices.SELECTION4):
                HighlightedSelection(3);
                if (selectThis)
                    selectCurrentPokemon(3);
                if (movementVector.y == 1)
                    currentChoice = menuChoices.SELECTION3;
                if (movementVector.y == -1)
                    currentChoice = menuChoices.SELECTION5;
                break;
            case (menuChoices.SELECTION5):
                HighlightedSelection(4);
                if (selectThis)
                    selectCurrentPokemon(4);
                if (movementVector.y == 1)
                    currentChoice = menuChoices.SELECTION4;
                if (movementVector.y == -1)
                    currentChoice = menuChoices.BACKBUTTON;
                break;
            case (menuChoices.BACKBUTTON):
                HighlightedSelection(10);
                if (selectThis && TBC.currentState == TurnBasedCombat.BattleStates.OUTOFCOMBATMENU)
                {
                    TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBAT;
                }
                else if (selectThis && TBC.currentState == TurnBasedCombat.BattleStates.MENU)
                {
                    TBC.currentState = TurnBasedCombat.BattleStates.PLAYERCHOICE;
                    BCSetup.UpdateBattleCanvas(playerPokedex, false, "", "");
                }
                if (movementVector.y == 1)
                    currentChoice = menuChoices.SELECTION5;
                if (movementVector.y == -1)
                    currentChoice = menuChoices.SELECTION1;
                break;
        }
        selectThis = false;
    }
}
