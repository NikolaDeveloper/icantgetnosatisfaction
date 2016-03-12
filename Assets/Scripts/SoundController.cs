using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public static SoundController Instance;

    public AudioClip hitObstacleClip, stopStationClip, moneyGainClip, screechBrake;

    AudioSource musicObj, trainLoopObj;

    float sfvol, mvol;

    void Awake()
    {
        Instance = this;

        mvol = PlayerPrefs.GetFloat("SatisfactionSettings_musicVolume", 0.8f);
        sfvol = PlayerPrefs.GetFloat("SatisfactionSettings_sfxVolume", 0.9f);
    }

    void Start()
    {
        musicObj = gameObject.GetComponents<AudioSource>()[0];
        trainLoopObj = gameObject.GetComponents<AudioSource>()[1];
        musicObj.volume = mvol;
        trainLoopObj.volume = sfvol;
    }

    public void MusicON()
    {
        musicObj.Play();
    }

    public void MusicOFF()
    {
        musicObj.Stop();
    }

    public void TrainMovingSoundON()
    {
        trainLoopObj.Play();
    }

    public void TrainMovingSoundOFF()
    {
        trainLoopObj.Stop();
    }

    public void PlayScreechBrakeSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(screechBrake, sfvol);
    }

    public void PlayMoneyGainSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(moneyGainClip, sfvol);
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
