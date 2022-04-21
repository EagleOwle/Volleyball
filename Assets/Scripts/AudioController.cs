using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] private AudioMixerGroup _sfxMixer;
    [Header("Music Clips")]
    [SerializeField] private AudioClip[] _musicClips;
    [Space]
    [Range(-60f, 0f)]
    [Tooltip("Music Volume, dB from -60 to 0")]
    [SerializeField] private float musicVolume = -12f;
    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    public static AudioController Instance;

    private const string SFXVolumeParamName = "SFXVolume";
    private const string MusicVolumeParamName = "MusicVolume";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _musicSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _musicSource.outputAudioMixerGroup = _musicMixer;
        _sfxSource.outputAudioMixerGroup = _sfxMixer;
    }

    private void Start()
    {
        //SceneLoader.OnSceneLoadStart += PauseMusic;
        //SceneLoader.OnSceneLoadComplete += ResumeMusic;

        if (_musicClips.Length == 0) return;

        _musicSource.clip = _musicClips[Random.Range(0, _musicClips.Length)];
        _musicSource.loop = true;
        _musicSource.Play();
    }

    private void PauseMusic()
    {
        _musicSource.Pause();
    }

    private void ResumeMusic()
    {
        _musicSource.Play();
    }

    public bool ToggleMusic()
    {
        _musicMixer.audioMixer.GetFloat(MusicVolumeParamName, out float value);

        if (value < musicVolume)
        {
            _musicMixer.audioMixer.SetFloat(MusicVolumeParamName, musicVolume);
            return true;
        }
        else
        {
            _musicMixer.audioMixer.SetFloat(MusicVolumeParamName, -80f);
            return false;
        }
    }

    public void ToggleMusic(bool enabled)
    {
        float value = enabled == true ? musicVolume : -80f;
        _musicMixer.audioMixer.SetFloat(MusicVolumeParamName, value);
    }

    public bool ToggleSFX()
    {
        _sfxMixer.audioMixer.GetFloat(SFXVolumeParamName, out float value);

        if (value < 0f)
        {
            _sfxMixer.audioMixer.SetFloat(SFXVolumeParamName, 0f);
            return true;
        }
        else
        {
            _sfxMixer.audioMixer.SetFloat(SFXVolumeParamName, -80f);
            return false;
        }
    }

    public void ToggleSFX(bool enabled)
    {
        float value = enabled == true ? 0f : -80f;
        _sfxMixer.audioMixer.SetFloat(SFXVolumeParamName, value);
    }

    private void OnDestroy()
    {
        //SceneLoader.OnSceneLoadStart -= PauseMusic;
        //SceneLoader.OnSceneLoadComplete -= ResumeMusic;
    }

    public void PlayClip(AudioClip clip, float volume = 1f, float pitch = 1f)
    {
        _sfxSource.clip = clip;
        _sfxSource.volume = volume;
        _sfxSource.spatialBlend = 0f;
        _sfxSource.Play();
    }

}
