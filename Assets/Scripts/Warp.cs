using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

    private PlayerMovement player;
    private Rigidbody2D playerRB;
    public Transform warpTarget;
    public PlayerMovement.PlayerZone playersZone;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        FreezePlayer(true);
        yield return StartCoroutine(sf.FadeToBlack());

        other.transform.position = warpTarget.transform.position;
        Camera.main.transform.position = warpTarget.transform.position;

        yield return StartCoroutine(sf.FadeToClear());

        player.currenZone = playersZone;

        FreezePlayer(false);
    }

    void FreezePlayer(bool freeze)
    {

        if (freeze)
        {
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            playerRB.constraints = RigidbodyConstraints2D.None;
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        
    }
}
