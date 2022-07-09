using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent (typeof(AudioSource))]
public class videoScript : MonoBehaviour {

    public MovieTexture introMovie;
    public AudioSource audio;
    private RawImage videoLayer;
    public AudioClip introMusic, loopMusic;
    public bool playedIntro = false, introMovieDone = false;
    public Animator pokemonLogo;

	// Use this for initialization
	void Start () {
        GetComponent<RawImage>().texture = introMovie as MovieTexture;
        gameObject.GetComponent<RawImage>().enabled = true;
        audio.clip = introMovie.audioClip;
        introMovie.Play();
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Submit") && !introMovieDone)
        {
            stopMoviePlaying();
        }

        else if (Input.GetButtonDown("Submit") && introMovieDone)
        {
            Application.LoadLevel(1);
        }
        //ends on its own 
        if (!introMovie.isPlaying && !introMovieDone)
        {
            stopMoviePlaying();
        }

        
        if (!audio.isPlaying && !playedIntro)
        {
            //audio.clip = introMusic;
            //audio.Play();
            audio.PlayOneShot(introMusic);
            playedIntro = true;
        }

        if (!audio.isPlaying && playedIntro && introMovieDone)
        {
            audio.clip = loopMusic;
            audio.loop = true;
            audio.Play();
        }

	}

    private void stopMoviePlaying()
    {
        gameObject.GetComponent<RawImage>().enabled = false;
        introMovieDone = true;
        StartCoroutine(playPokemonFlash());
        audio.Stop();
    }

    private IEnumerator playPokemonFlash()
    {
        while (introMovieDone)
        {
            //play animation
            pokemonLogo.Play("flashPokemon", -1, 0.0f);
            Debug.Log("played animation");
            yield return new WaitForSeconds(10);
        }
    }
}
