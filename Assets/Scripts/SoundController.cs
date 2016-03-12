using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public static SoundController Instance;

    public AudioClip hitObstacleClip, stopStationClip;

    AudioSource musicObj;

    float sfvol, mvol;

    void Awake()
    {
        Instance = this;

        mvol = PlayerPrefs.GetFloat("SatisfactionSettings_musicVolume", 0.8f);
        sfvol = PlayerPrefs.GetFloat("SatisfactionSettings_sfxVolume", 0.9f);
    }

    void Start()
    {
        musicObj = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        musicObj.volume = mvol;
    }

	void Update()
    {
	
	}

    public void PlayObstacleHitSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(hitObstacleClip, sfvol);
    }

    public void PlayStopAtStationSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(stopStationClip, sfvol);
    }
}
