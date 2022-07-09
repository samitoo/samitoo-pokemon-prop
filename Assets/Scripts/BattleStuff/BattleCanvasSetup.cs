using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Canvas
using System;
using System.Collections.Generic; //Lists


public class BattleCanvasSetup : MonoBehaviour {

    private ScrollingBattleText scrollingBattleText;
    public PokedexXML playerPokedex;
    public Text battleTextPanel;
    public Text ppName, ppLevel, enemyName, enemyLevel;
    public Image ppImage, enemyImage;


    void Awake()
    {
        //Moved to awake b/c this item is set to inactive on Start() from BattleSceneSetup
        playerPokedex = GameObject.FindGameObjectWithTag("Player").GetComponent<PokedexXML>();
        scrollingBattleText = gameObject.GetComponent<ScrollingBattleText>();
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateBattleCanvas(PokedexXML pokedex, bool enemy, String front, String back)
    {
        UpdatePlayerInfo();
        if(enemy)
            UpdateEnemyInfo(pokedex, front, back);
    }

    private void UpdatePlayerInfo()
    {
        ppName.text = (playerPokedex.currentPokemon.name).ToUpper();
        ppLevel.text = (playerPokedex.currentPokemon.level).ToString();
        //Debug.Log(playerPokedex.currentPokemon.imageBack);
        ppImage.sprite = Resources.Load<Sprite>(playerPokedex.currentPokemon.imageBack);
    }

    private void UpdateEnemyInfo(PokedexXML pokedex, String front, String back)
    {
        //Have to specify which random b/c import System and Unity Engine
        List <PokemonXML> pokeDexArray = pokedex.pcXML.Pokemon;
        //Random Pokemon that still has HP
        int count = 0;
        while (pokedex.currentPokemon.currentHp <= 0)
        {
            pokedex.currentPokemon = pokeDexArray[UnityEngine.Random.Range(0, pokeDexArray.Count)];
            count++;
            if (count > pokeDexArray.Count)
                return;
        }
        enemyName.text = (pokedex.currentPokemon.name).ToUpper();
        enemyLevel.text = (pokedex.currentPokemon.level).ToString();
        enemyImage.sprite = Resources.Load<Sprite>(pokedex.currentPokemon.imageFront);
        string battleText = front + " " + pokedex.currentPokemon.name.ToString().ToUpper() + " " + back;
        //string battleText = "A wild " + pokedex.currentPokemon.name.ToString().ToUpper() +" has appeared!";
        scrollingBattleText.setText(battleText);
    }


}
