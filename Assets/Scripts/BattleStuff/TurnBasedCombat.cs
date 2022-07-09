using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnBasedCombat : MonoBehaviour {

    public enum BattleStates
    {
        OUTOFCOMBAT,
        START,
        PLAYERCHOICE,
        ENEMYCHOICE,
        INCOMBAT,
        MENU,
        OUTOFCOMBATMENU,
        LOSE,
        WIN,
        BATTLEEND,
        BOSSBATTLEFINISH
    }
    public BattleStates currentState;
    public GameObject BattleScreen, PlayerCommands, TextPanel, pokeMenu, pokeMenuText, finalScene;
    private BattleSceneSetup battleScene;
    private bool swapCameras; //to control coroutine calls

	// Use this for initialization
	void Start () {
        currentState = BattleStates.OUTOFCOMBAT;
        battleScene = FindObjectOfType<BattleSceneSetup>();
	}
	
	// Update is called once per frame
	void Update () {

        CheckState();
	}

    void CheckState()
    {
        switch (currentState)
        {
            case(BattleStates.OUTOFCOMBAT):
                PlayerCommands.SetActive(false);
                TextPanel.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                finalScene.SetActive(false);
                break;
            case (BattleStates.START):
                swapCameras = true;
                PlayerCommands.SetActive(false);
                TextPanel.SetActive(true);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                finalScene.SetActive(false);
                break;
            case (BattleStates.PLAYERCHOICE):
                PlayerCommands.SetActive(true);
                TextPanel.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                finalScene.SetActive(false);
                break;
            case(BattleStates.ENEMYCHOICE):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                TextPanel.SetActive(true);
                finalScene.SetActive(false);
                break;
            case(BattleStates.INCOMBAT):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                TextPanel.SetActive(true);
                finalScene.SetActive(false);
                break;
            case(BattleStates.MENU):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(true);
                pokeMenuText.SetActive(true);
                TextPanel.SetActive(false);
                finalScene.SetActive(false);
                break;
            case(BattleStates.OUTOFCOMBATMENU):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(true);
                pokeMenuText.SetActive(true);
                TextPanel.SetActive(false);
                finalScene.SetActive(false);
                break;
            case(BattleStates.LOSE):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                TextPanel.SetActive(true);
                finalScene.SetActive(false);
                break;
            case(BattleStates.WIN):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                TextPanel.SetActive(true);
                finalScene.SetActive(false);
                //change camera back
                //BattleSceneSetup coroutine
                break;
            case(BattleStates.BATTLEEND):
                PlayerCommands.SetActive(false);
                TextPanel.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                finalScene.SetActive(false);
                if (swapCameras)
                {
                    StartCoroutine(battleScene.fadeCameraChange(true, false));
                }
                swapCameras = false;
                break;
            case(BattleStates.BOSSBATTLEFINISH):
                PlayerCommands.SetActive(false);
                pokeMenu.SetActive(false);
                pokeMenuText.SetActive(false);
                TextPanel.SetActive(true);
                finalScene.SetActive(true);
                break;
        }
    }
}
