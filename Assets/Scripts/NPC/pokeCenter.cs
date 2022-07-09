using UnityEngine;
using System.Collections;

public class pokeCenter : MonoBehaviour {

    public AudioClip pokeHeal;
    private GameObject player;
    private Animator nurseAnim, pokeAnim;
    private AudioSource audio;
    private bool playedHeal = false;
    private PokedexXML playerPokedex;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPokedex = player.GetComponent<PokedexXML>();
        nurseAnim = gameObject.GetComponent<Animator>();
        pokeAnim = GameObject.FindGameObjectWithTag("pokeballs").GetComponent<Animator>();
        audio = GameObject.FindObjectOfType<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Nurse found Tasha!");
        }
        playedHeal = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Submit") && !playedHeal)
        {
            Debug.Log("heals");
            nurseAnim.SetTrigger("startHeal");
            pokeAnim.SetTrigger("healPokeballs");
            audio.Stop();
            audio.PlayOneShot(pokeHeal);
            healAllPokemon();
            playedHeal = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        nurseAnim.SetTrigger("endHeal");
        pokeAnim.SetTrigger("stopHeals");
        if(playedHeal)
            audio.Play();
        
    }

    private void healAllPokemon()
    {
        foreach (PokemonXML poke in playerPokedex.pcXML.Pokemon)
        {
            poke.currentHp = poke.hp;
        }
    }
}
