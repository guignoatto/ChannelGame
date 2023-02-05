using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            AkSoundEngine.SetRTPCValue("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            //Debug.Log(PlayerPrefs.GetFloat("MasterVolume"));
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            AkSoundEngine.SetRTPCValue("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            //Debug.Log(PlayerPrefs.GetFloat("MusicVolume"));
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            AkSoundEngine.SetRTPCValue("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            //Debug.Log(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

   
}
