using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePreference", menuName = "GamePreference")]
public class Preference : ScriptableObject
{
    private static Preference singleton;
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

    private float _musicValue = 1;
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
    public const float musicValueDef = 1;
    public Action<float> actionOnChangeMusicValue;

    private float _sfxValue = 1;
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
    public const float sfxValueDef = 1;
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
    public const bool onVibrationDef = true;
    public Action<bool> actionOnChangeVibration;


    [Header("Input")]
    public float InputDeadZone = 10;
    public const float InputDeadZoneDef = 10;
    public InputType inputType = InputType.button;

    [Header("UnitMotion")]
    public float moveSpeed = 250;
    public const float moveSpeedDef = 250;
    public const float jumpForceDef = 250;
    public float jumpForce = 250;
    public const float horizontalMultiplyDef = 1.5f;
    public float horizontalMultiply = 1.5f;
    public const float downSpeedDef = 20;
    public float downSpeed = 20;

    [Header("AiLogic")]
    public float deadzoneForMove = 0.3f;
    public const float deadzoneForMoveDef = 0.3f;
    public const float deadzoneForJumpDef = 3;
    public float deadzoneForJump = 3;

    [Header("Ball")]
    public float pushForce = 25f;
    public const float pushForceDef = 25;
    public const float maxMagnetudeDef = 5;
    public float maxMagnetude = 5;

    public void SetDefaultValue()
    {
        MusicValue = musicValueDef;
        SfxValue = sfxValueDef;
        InputDeadZone = InputDeadZoneDef;
        moveSpeed = moveSpeedDef;
        jumpForce = jumpForceDef;
        horizontalMultiply = horizontalMultiplyDef;
        downSpeed = downSpeedDef;
        deadzoneForMove = deadzoneForMoveDef;
        deadzoneForJump = deadzoneForJumpDef;
        pushForce = pushForceDef;
        maxMagnetude = maxMagnetudeDef;

    }

}
