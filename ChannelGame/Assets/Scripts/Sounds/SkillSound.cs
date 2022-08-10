using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSound : SoundBase
{
   [SerializeField] private AudioClip _skillAudioClip;
   
   public AudioClip SkillAudioClip { get { return _skillAudioClip; } }
}
