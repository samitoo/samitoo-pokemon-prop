using UnityEngine;
using System.Collections;

public class EncounterZone : MonoBehaviour {

    public bool gymFight;
    public string gymLeader;
    public int numberToFight = 1;
    public float enemySpawn;
    private PlayerMovement player;
    private BattleSceneSetup battleScene;
    private bool inTheGrass = false;
    public BattleCanvasSetup BCSetup;
    private PokedexXML pokeDex;
    private Fight fight;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        battleScene = FindObjectOfType<BattleSceneSetup>();
        //BCSetup = FindObjectOfType<BattleCanvasSetup>();
        pokeDex = gameObject.GetComponent<PokedexXML>();
        fight = FindObjectOfType<Fight>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region Wild Encounters
    void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // text box is over when player can move again? 
            if (gymFight)
            {
                StartCoroutine(CheckForBossFightStart());
            }
            else
            {
                Debug.Log("In Tall Grass");
                inTheGrass = true;
                StartCoroutine(CheckForEnemySpawn());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !gymFight)
        {
                Debug.Log("Leaving Grass");
                inTheGrass = false;
                StopCoroutine(CheckForEnemySpawn());
                enemySpawn = 0;
        }
    }

    public void StartEncounter()
    {
        Debug.Log("enoutered");
        StartCoroutine(battleScene.fadeCameraChange(false, true));
        //Call update battle canvas
        if (gymFight)
        {
            BCSetup.UpdateBattleCanvas(pokeDex, true, gymLeader + " throws out", "");
            fight.numberToFight = numberToFight;
        }
        else
            BCSetup.UpdateBattleCanvas(pokeDex, true, "A wild", "appears");
        //resets Enemy HP bar
        fight.battleStarted = true;
        //Loads pokedex for fight
        fight.LoadEnemyPokedex(pokeDex);

    }

    //Check for enounter every 'yield' seconds 
    IEnumerator CheckForEnemySpawn()
    {
        while (inTheGrass)
        {
            enemySpawn = Random.Range(1, 4);
            if (enemySpawn == 3 && player.isMoving)
                StartEncounter();
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator CheckForBossFightStart()
    {
        while (!player.canMove)
        {
            yield return new WaitForSeconds(3);
        }
        StartEncounter();
    }

    #endregion
}
