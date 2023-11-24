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
            var currentValue = defaultMusicValue;
            if (PlayerPrefs.HasKey("MusicValue"))
            {
                currentValue = PlayerPrefs.GetFloat("MusicValue");
            }

            if (value != currentValue)
            {
                currentValue = value;
                PlayerPrefs.SetFloat("MusicValue", currentValue);
                actionOnChangeMusicValue?.Invoke(currentValue);
            }
        }

        get
        {
            var currentValue = defaultMusicValue;
            if (PlayerPrefs.HasKey("MusicValue"))
            {
                currentValue = PlayerPrefs.GetFloat("MusicValue");
            }
            return currentValue;
        }
    }
    private const float defaultMusicValue = 0.4f;
    public Action<float> actionOnChangeMusicValue;

    public float SfxValue
    {
        set
        {
            var currentValue = defaultSfxValue;
            if (PlayerPrefs.HasKey("SfxValue"))
            {
                currentValue = PlayerPrefs.GetFloat("SfxValue");
            }
            if (value != currentValue)
            {
                currentValue = value;
                PlayerPrefs.SetFloat("SfxValue", currentValue);
                actionOnChangeSfxValue?.Invoke(currentValue);
            }
        }

        get
        {
            var currentValue = defaultSfxValue;
            if (PlayerPrefs.HasKey("SfxValue"))
            {
                currentValue = PlayerPrefs.GetFloat("SfxValue");
            }
            return currentValue;
        }
    }
    private const float defaultSfxValue = 0.6f;
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

    [Space()]
    public Language language;

    [Space()]
    public List<KeyCode> AllowedKeys = new List<KeyCode>()
    {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.T,
        KeyCode.Y,
        KeyCode.U,
        KeyCode.I,
        KeyCode.O,
        KeyCode.P,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,
        KeyCode.B,
        KeyCode.N,
        KeyCode.M,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.Backspace,
        KeyCode.LeftShift,
        KeyCode.LeftAlt,
        KeyCode.LeftControl,
        KeyCode.RightShift,
        KeyCode.RightAlt,
        KeyCode.RightControl,
        KeyCode.Keypad0,
        KeyCode.Keypad1,
        KeyCode.Keypad2,
        KeyCode.Keypad3,
        KeyCode.Keypad4,
        KeyCode.Keypad5,
        KeyCode.Keypad6,
        KeyCode.Keypad7,
        KeyCode.Keypad8,
        KeyCode.Keypad9,
        KeyCode.KeypadEnter,
        KeyCode.KeypadEquals,
        KeyCode.KeypadMinus,
        KeyCode.KeypadMultiply,
        KeyCode.KeypadPeriod,
        KeyCode.KeypadPlus,
        KeyCode.End,
        KeyCode.Home,
        KeyCode.PageUp,
        KeyCode.PageDown,
        KeyCode.Insert
    };

    [Space()]
    public PlayerPreference[] player;

    [Header("UnitMotion")]
    public float moveSpeed = 250;
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
