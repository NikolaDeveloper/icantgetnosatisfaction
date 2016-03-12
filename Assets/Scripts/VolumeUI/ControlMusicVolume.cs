using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlMusicVolume : MonoBehaviour {

    float mvol;

    Slider slider;

    void Awake()
    {
        mvol = PlayerPrefs.GetFloat("SatisfactionSettings_musicVolume", 0.8f);

        slider = gameObject.GetComponentInChildren<Slider>();
        slider.value = mvol;
    }

    public void VolumeSet()
    {
        mvol = slider.value;
        PlayerPrefs.SetFloat("SatisfactionSettings_musicVolume", mvol);
    }

    void Update()
    {

    }
}
