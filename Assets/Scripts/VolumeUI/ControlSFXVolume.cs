using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlSFXVolume : MonoBehaviour {

    float sfvol;

    Slider slider;

    void Awake()
    {
        sfvol = PlayerPrefs.GetFloat("SatisfactionSettings_sfxVolume", 0.9f);

        slider = gameObject.GetComponentInChildren<Slider>();
        slider.value = sfvol;
    }

    public void VolumeSet()
    {
        sfvol = slider.value;
        PlayerPrefs.SetFloat("SatisfactionSettings_sfxVolume", sfvol);
    }

    void Start()
    {
        
    }
}
