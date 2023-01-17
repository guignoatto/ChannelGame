using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeVolume : MonoBehaviour
{
    public Slider thisSlider;
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;

    public TMP_Text mastLabel, musicLabel, sfxLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(string whatValue) 
    {
        float sliderValue = thisSlider.value;

        if (whatValue == "Master") 
        {
            masterVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
            setMasterLab();
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }

        if (whatValue == "Music")
        {
            musicVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
            setMusicLab();
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }

        if (whatValue == "SFX")
        {
            SFXVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("SFXVolume", SFXVolume);
            setSFXLab();
            PlayerPrefs.SetFloat("SFXVolume", SFXVolume);
        }


    }

    public void setMasterLab() 
    {
        mastLabel.text = Mathf.RoundToInt(masterVolume * 100).ToString();
    }
    public void setMusicLab()
    {
        musicLabel.text = Mathf.RoundToInt(musicVolume * 100).ToString();
    }
    public void setSFXLab()
    {
        sfxLabel.text = Mathf.RoundToInt(SFXVolume * 100).ToString();
    }
}
