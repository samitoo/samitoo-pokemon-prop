using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleSceneSetup : MonoBehaviour {

    public Camera gameCamera, battleCamera;
    GameObject gameCameraObject, battleCameraObject, battleCanvas;
    PlayerMovement player;
    ScreenFader sf;
    PlayerTurnActions turnActions;
    BattleCanvasSetup BCSetup;
    private TurnBasedCombat TBC;

    public bool callFromMenu = false;
    public bool inMenu = false;
    private ZoneChangeHandler music;

    //Scene elements
    private PokedexXML playerPokeDex;

    TurnBasedCombat.BattleStates temp;

	// Use this for initialization
    void Start(){
        gameCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        battleCameraObject = GameObject.FindGameObjectWithTag("BattleCamera");
        battleCanvas = GameObject.FindGameObjectWithTag("BattleCanvas");
        sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        player = FindObjectOfType<PlayerMovement>();
        turnActions = FindObjectOfType<PlayerTurnActions>();
        BCSetup = FindObjectOfType<BattleCanvasSetup>();
        TBC = FindObjectOfType<TurnBasedCombat>();
        TBC.currentState = TurnBasedCombat.BattleStates.START;
        
        //Can't find game object thats not active, don't want two audio listeners.  Find, then inactive
        battleCameraObject.SetActive(false);
        battleCanvas.SetActive(false);
        //StartCoroutine(fadeCameraChange(true, false, true));

        temp = TurnBasedCombat.BattleStates.WIN;
    }
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            //changeCamera(true, false);
            StartCoroutine(fadeCameraChange(true, false));
            TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBAT;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            //changeCamera(false, true);
            StartCoroutine(fadeCameraChange(false, true));
        }

        //If in combat, dont use the menu
        //TODO REMOVE ME
        
        if (temp != TBC.currentState)
            Debug.Log(TBC.currentState);
        temp = TBC.currentState;
        
        if (TBC.currentState == TurnBasedCombat.BattleStates.OUTOFCOMBAT
            || TBC.currentState == TurnBasedCombat.BattleStates.OUTOFCOMBATMENU)
        {
            if (Input.GetButtonDown("Menu") && player.visitedOaksLab && !inMenu)
            {
                callFromMenu = true;
                changeCamera(false, true);
                TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBATMENU;
                inMenu = true;
            }
            else if (Input.GetButtonDown("Menu") && inMenu)
            {
                inMenu = false;
                changeCamera(true, false);
                TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBAT;
            }
        }
	}

    #region Camera change stuff 
    private void changeCamera(bool mainCam, bool battleCam)
    {
        Debug.Log("call change camera");
        
        gameCameraObject.SetActive(mainCam);
        gameCamera.enabled = mainCam;

        battleCameraObject.SetActive(battleCam);
        battleCamera.enabled = battleCam;
        battleCanvas.SetActive(battleCam);
        
        if (callFromMenu)
        {
            //turnActions.SetPlayerChoice(PlayerTurnActions.playerChoices.FIGHT);
            player.canMove = false; 
        }

        if (mainCam)
            player.canMove = true;
        else if (!mainCam && !callFromMenu)
        {
            player.canMove = false;
            turnActions.SetPlayerChoice(PlayerTurnActions.playerChoices.FIGHT);
            TBC.currentState = TurnBasedCombat.BattleStates.START;
            //BCSetup.UpdateBattleCanvas();
        }
        callFromMenu = false;
    }

    //Fade and change camera 
    public IEnumerator fadeCameraChange(bool mainCam, bool battleCam)
    {
        //Moved here because Tallgrass loads the pokemon before the fade in 
        battleCanvas.SetActive(battleCam);
        //Fade out
        yield return StartCoroutine(sf.FadeToBlack());
        changeCamera(mainCam, battleCam);
        if (mainCam)
            TBC.currentState = TurnBasedCombat.BattleStates.OUTOFCOMBAT;
        //Fade in
        yield return StartCoroutine(sf.FadeToClear());

    }

    #endregion 
}
