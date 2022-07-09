using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoneChangeHandler : MonoBehaviour {

    public AudioSource audioSource;
    private PlayerMovement.PlayerZone playersZone, previousZone = PlayerMovement.PlayerZone.GYM;
    private PlayerMovement player;
    public AudioClip heroHome, palletCity, oaksLab, route1, viridianCity, pokeCenter, gym;
    private bool exitLoadMusic = false, playedOaksTheme = false;
    public Text zoneText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (Application.loadedLevel == 1 && !playedOaksTheme)
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = oaksLab;
            audioSource.Play();
            playedOaksTheme = true;
        }

        if (Application.loadedLevel == 2)
        {
            playersZone = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currenZone;

            if (!exitLoadMusic)
            {
                zoneText = GameObject.FindGameObjectWithTag("ZoneText").GetComponent<Text>();
                audioSource.Stop();
                audioSource.clip = heroHome;
                audioSource.Play();
                exitLoadMusic = true;
            }
        }

        if(Application.loadedLevel == 2 && playersZone != previousZone)
        {
            SwitchZones();
        }

        
	}

    public void SwitchZones()
    {
        //set previous zone when updating in the method
        previousZone = playersZone;
        zoneText.GetComponentInParent<Animator>().SetTrigger("zonePopIn");
        audioSource.Stop();
        switch (playersZone)
        {
            case (PlayerMovement.PlayerZone.HEROHOME):
                zoneText.text = "TASHAS HOME";
                audioSource.clip = heroHome;
                audioSource.Play();
                break;
            case (PlayerMovement.PlayerZone.PALLETTOWN):
                zoneText.text = "PALLET TOWN";
                audioSource.clip = palletCity;
                audioSource.Play();
                break;
            case (PlayerMovement.PlayerZone.OAKSLAB):
                zoneText.text = "OAKS LAB";
                audioSource.clip = oaksLab;
                audioSource.Play();
                break;
            case (PlayerMovement.PlayerZone.ROUTE1):
                zoneText.text = "ROUTE 1";
                audioSource.clip = route1;
                audioSource.Play();
                break;
            case (PlayerMovement.PlayerZone.VIRIDIANCITY):
                zoneText.text = "VIRIDIAN CITY";
                audioSource.clip = viridianCity;
                audioSource.Play();
                break;
            case (PlayerMovement.PlayerZone.POKECENTER):
                zoneText.text = "POKE CENTER";
                audioSource.clip = pokeCenter;
                audioSource.Play();
                break;
            case (PlayerMovement.PlayerZone.GYM):
                zoneText.text = "SAMIS GYM";
                audioSource.clip = gym;
                audioSource.Play();
                break;
        }
    }
}
