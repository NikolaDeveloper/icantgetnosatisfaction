using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public static SoundController Instance;

    public AudioClip hitObstacleClip, stopStationClip, moneyGainClip, screechBrake;

    AudioSource trainLoopObj;

    float sfvol;

    void Awake()
    {
        Instance = this;
        sfvol = PlayerPrefs.GetFloat("SatisfactionSettings_sfxVolume", 0.9f);
    }

    void Start()
    {
        trainLoopObj = gameObject.GetComponents<AudioSource>()[0];
		trainLoopObj.volume = sfvol;
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
        gameObject.GetComponents<AudioSource>()[1].PlayOneShot(screechBrake, sfvol * 1.2f);
    }

    public void PlayMoneyGainSound()
    {
        gameObject.GetComponents<AudioSource>()[2].PlayOneShot(moneyGainClip, sfvol * 0.7f);
    }

    public void PlayObstacleHitSound()
    {
        gameObject.GetComponents<AudioSource>()[2].PlayOneShot(hitObstacleClip, sfvol * 0.9f);
    }

    public void PlayStopAtStationSound()
    {
        gameObject.GetComponents<AudioSource>()[2].PlayOneShot(stopStationClip, sfvol * 1.3f);
    }
}
