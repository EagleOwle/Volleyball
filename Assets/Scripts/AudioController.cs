using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Music Clips")]
    [SerializeField] private AudioClip _musicClip;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    public static AudioController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        _musicSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        SceneLoader.OnSceneLoadStart += PauseMusic;
        SceneLoader.OnSceneLoadComplete += ResumeMusic;
        Preference.Singleton.actionOnChangeMusicValue += OnChangeMusicValue;
        Preference.Singleton.actionOnChangeSfxValue += OnChangeSfxValue;

        _musicSource.clip = _musicClip;
        _musicSource.loop = true;
        OnChangeMusicValue(Preference.Singleton.MusicValue);
        _musicSource.Play();
    }

    private void OnChangeMusicValue(float value)
    {
        _musicSource.volume = value;
    }

    private void OnChangeSfxValue(float value)
    {
        _sfxSource.volume = value;
    }

    private void PauseMusic()
    {
        _musicSource.Pause();
    }

    private void ResumeMusic()
    {
        _musicSource.Play();
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadStart -= PauseMusic;
        SceneLoader.OnSceneLoadComplete -= ResumeMusic;
        Preference.Singleton.actionOnChangeMusicValue -= OnChangeMusicValue;
    }

    public void PlayClip(AudioClip clip, bool replay = true)
    {
        if (replay == false)
        {
            if (_sfxSource.isPlaying)
                return;
        }

        _sfxSource.clip = clip;
        _sfxSource.Play();
        //_sfxSource.PlayOneShot(clip);
    }

}
