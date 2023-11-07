using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    Zh = 0,
    Eng = 1,
    Rus = 2
    
}

[CreateAssetMenu(fileName = "GamePreference", menuName = "GamePreference")]
public class Preference : ScriptableObject
{
    public static Preference Singleton
    {
        get
        {
            if(singleton == null)
            {
                singleton = Resources.Load("GamePreference") as Preference;
            }

            return singleton;
        }
    }
    private static Preference singleton;

    public float MusicValue
    {
        set
        {
            if (value != _musicValue)
            {
                _musicValue = value;
                actionOnChangeMusicValue?.Invoke(_musicValue);
            }
        }

        get
        {
            return _musicValue;
        }
    }
    private float _musicValue = 1;
    public Action<float> actionOnChangeMusicValue;

    public float SfxValue
    {
        set
        {
            if (value != _sfxValue)
            {
                _sfxValue = value;
                actionOnChangeSfxValue?.Invoke(_sfxValue);
            }
        }

        get
        {
            return _sfxValue;
        }
    }
    private float _sfxValue = 1;
    public Action<float> actionOnChangeSfxValue;

    [Header("Vibra")]
    public bool _onVibration = true;
    public bool Vibration
    {
        set
        {
            if (value != _onVibration)
            {
                _onVibration = value;
                actionOnChangeVibration?.Invoke(_onVibration);
            }
        }

        get
        {
            return _onVibration;
        }
    }
    public Action<bool> actionOnChangeVibration;

    public Language language;

    [Header("Input")]
    public float InputDeadZone = 10;
    public InputType inputType = InputType.button;

    [Header("UnitMotion")]
    public float moveSpeed = 250;
    public float jumpMoveSpeed = 250;
    public float jumpForce = 250;
    public float downSpeed = 20;
    public float pushForce = 25f;

    [Header("AiLogic")]
    public float deadzoneForMove = 0.3f;

    [Header("Trajectory")]
    public TrajectoryRender trajectoryRenderPrefab;

    [Header("Ball")]
    public BallSetting[] balls;

    [System.Serializable]
    public class BallSetting
    {
        public string neme;
        public float maxMagnetude = 5;
        public float maxAngularMagnitude = 60;
        public int hitClipsCount
        {
            get
            {
                return hitClip.Length;
            }
        }
        public AudioClip[] hitClip;
        public Ball prefab;
    }
}
