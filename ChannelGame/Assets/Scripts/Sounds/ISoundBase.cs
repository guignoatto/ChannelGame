using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISoundBase
{
    public void SetUpBgSong();
    public void PlayBgSong();
    public void StopBgSong();
    public void PlaySoundEffect(AudioClip clip, float volume = 1, float pitch = 1);
}
