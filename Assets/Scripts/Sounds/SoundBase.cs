using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class SoundBase : MonoBehaviour, ISoundBase
{

    [SerializeField] private GameObject _prefab;
    [Space]
    [Header("General Sounds")] 
    [SerializeField] private AudioClip _bgSong;
    [SerializeField] private List<AudioClip> _onEnableSounds;

    [Header("Volume")]
    [Range(0f,1f)]
    [SerializeField] private float _onEnableVolume = 1f;
    [Range(0f,1f)]
    [SerializeField] private float _bgVolume = 1f;

    [Header("Mixer")]
    [SerializeField] private AudioMixerGroup _soundEffectGroup;
    [SerializeField] private AudioMixerGroup _bgGroup;


    protected AudioSource _audioSource;

    public virtual void OnEnable()
    {
        if (_onEnableSounds != null)
        {
            foreach (var sound in _onEnableSounds)
            {
                PlaySoundEffect(sound, _onEnableVolume);
            }
        }
        SetUpBgSong();
    }

    public void SetUpBgSong()
    {
        if (_bgSong != null)
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _bgSong;
            _audioSource.volume = _bgVolume;
            _audioSource.outputAudioMixerGroup = _bgGroup;
            _audioSource.Play();
        }
    }
    public void PlayBgSong()
    {
        _audioSource.Play();
    }
    public void StopBgSong()
    {
        _audioSource.Stop();
    }
    public void PlaySoundEffect(AudioClip clip, float volume = 1, float pitch = 1)
    {
        AudioSource aSource = Instantiate(_prefab , gameObject.transform).AddComponent<AudioSource>();
        aSource.playOnAwake = false;
        aSource.pitch = pitch;
        aSource.clip = clip;
        aSource.volume = volume;
        aSource.outputAudioMixerGroup = _soundEffectGroup;
        aSource.Play();
        Destroy(aSource.gameObject, clip.length + 1);
    }
}
