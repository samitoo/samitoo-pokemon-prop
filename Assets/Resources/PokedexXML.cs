using UnityEngine;
using System.Collections;
using System.IO;

public class PokedexXML : MonoBehaviour {

    public string path = "samiPokedex";
    public PokemonXML currentPokemon;
    public PokemonContainerXML pcXML;

	// Use this for initialization
	void Start () {

        pcXML = PokemonContainerXML.LoadFromText(path);
        currentPokemon = pcXML.Pokemon[0];
       // Debug.Log(currentPokemon.name);
        
        /*foreach (PokemonXML poke in pcXML.Pokemon)
        {
            Debug.Log(poke.name);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
