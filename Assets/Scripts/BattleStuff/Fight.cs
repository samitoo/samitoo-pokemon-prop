using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fight : MonoBehaviour {

    public PokedexXML enemyPokedex;
    private PokedexXML playerPokedex;
    private TurnBasedCombat TBC;
    //To save values only, you set CurrentPokemon through their Pokedex
    private PokemonXML playerPokemon, enemyPokemon;  
    public ScrollingBattleText battleText;
    public int basePower = 65;
    public bool battleStarted;
    public bool enemyFainted = false;
    public int numberToFight = 1;
    public Image enemyHPBar, playerHPBar;
    public Animator playerAnimator, enemyAnimator;
    public BattleCanvasSetup BCSetup;
    public FinalScene finalSceneScript;
    

	// Use this for initialization
	void Start () {
        TBC = FindObjectOfType<TurnBasedCombat>();
        playerPokedex = GameObject.FindGameObjectWithTag("Player").GetComponent<PokedexXML>();
       
        playerHPBar.fillAmount = 1f;
        enemyHPBar.fillAmount = 1f;
        battleStarted = true;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (battleStarted)
        {
            enemyHPBar.fillAmount = 1f;
            battleStarted = false;
        }
	}

    public void LoadEnemyPokedex(PokedexXML loadingPokedex)
    {
        enemyPokedex = loadingPokedex;
    }

    public void PlayerAttack()
    {
        playerPokemon = playerPokedex.currentPokemon;
        enemyPokemon = enemyPokedex.currentPokemon;
        battleText.setText(playerPokemon.name.ToUpper() + "  attacked  " + enemyPokemon.name.ToUpper() + "  !  ");

        playerAnimator.Play("playerAttack");
        enemyPokemon.currentHp = enemyPokemon.currentHp - DamageFormula(playerPokemon, enemyPokemon);
        if (enemyPokemon.currentHp <= 0)
        {
            enemyFainted = true;
            numberToFight -= 1;
        }
        StartCoroutine(isAttackTextOver(TurnBasedCombat.BattleStates.ENEMYCHOICE));
        
    }

    private void EnemyAttack()
    {
        playerPokemon = playerPokedex.currentPokemon;
        enemyPokemon = enemyPokedex.currentPokemon;
        battleText.setText(enemyPokemon.name.ToUpper() + "  attacked  " + playerPokemon.name.ToUpper() + "  !  ");
        playerPokemon.currentHp = playerPokemon.currentHp - (DamageFormula(enemyPokemon, playerPokemon) / 4);
        int enemyAttackType = Random.Range(0, 1);
        if (enemyAttackType == 0)
        {
            enemyAnimator.SetTrigger("enemyAttack");
        }
        else
            enemyAnimator.SetTrigger("enemySlideAttack");
        //TODO: When player is below 0, this stop his sprite from updating for some reason
        //if(playerPokemon.currentHp <= 0){
        //    TBC.currentState = TurnBasedCombat.BattleStates.MENU;
        //    return;
        //}
        StartCoroutine(isAttackTextOver(TurnBasedCombat.BattleStates.PLAYERCHOICE));
    }

    private void Faint(bool Player)
    {
    }

    //Allow the scrolling combat text to finish before changing battle states
    private IEnumerator isAttackTextOver(TurnBasedCombat.BattleStates stateToSet)
    {
        while (battleText.isTyping)
        {
            yield return new WaitForSeconds(2);
        }
        TBC.currentState = stateToSet;
        //Update HP bars after text finishes
        enemyHPBar.fillAmount = ((float)enemyPokemon.currentHp / (float)enemyPokemon.hp);
        playerHPBar.fillAmount = ((float)playerPokemon.currentHp / (float)playerPokemon.hp);
        //call enemy attack
        if (stateToSet == TurnBasedCombat.BattleStates.ENEMYCHOICE)
        {
            if (!enemyFainted)
            {
                EnemyAttack();
            }
            //See if all enemies are dead
            else if (enemyFainted && numberToFight > 0)
            {
                BCSetup.UpdateBattleCanvas(enemyPokedex, true, "Sami throws out", "");
                enemyFainted = false;
                battleStarted = true;
                StartCoroutine(isAttackTextOver(TurnBasedCombat.BattleStates.ENEMYCHOICE));
                //WinTheRound();
            }
            else if (enemyFainted && numberToFight <= 0)
            {
                WinTheRound();
            }

        }
    
    }


    private void WinTheRound()
    {
        enemyFainted = false;
        TBC.currentState = TurnBasedCombat.BattleStates.WIN;
        if (enemyPokedex.path == "samiPokedex")
        {
            battleText.setText("Tasha defeated Sami!");
            StartCoroutine(isAttackTextOver(TurnBasedCombat.BattleStates.BOSSBATTLEFINISH));
            finalSceneScript.startFinal = true;
        }
        else
        {
            battleText.setText("Tasha defeated " + enemyPokemon.name.ToUpper());
            StartCoroutine(isAttackTextOver(TurnBasedCombat.BattleStates.BATTLEEND));
            //TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBAT;
        }
            
        
    }
    //Based off the Bulbapedia damage chart
    //http://bulbapedia.bulbagarden.net/wiki/Damage#Damage_formula
    public int  DamageFormula(PokemonXML playersPokemon, PokemonXML enemiesPokemon)
    {
        //Demo values left to the right for Bulba debug
        float Level = playerPokemon.level; // 75 
        float Attack = playerPokemon.attack; //123
        float Defense = enemiesPokemon.defense; //163
        float damage;
        //Same type attack bonus * Type Effectiveness * Crit * Other (items)
        float modifier = 1.5f * 4 * 1 * 1;
        float randomMin = .85f * modifier;
        float randomMax = 1 * modifier;

        damage = ((2 * Level + 10) * Attack) / (250 * Defense);
        damage = damage * basePower + 2;
        randomMin *= damage;
        randomMax *= damage;

        return (int)Mathf.Floor(Random.Range(randomMin, randomMax));
    }


}
