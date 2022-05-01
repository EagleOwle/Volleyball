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

    [Header("Sound")]
    public const float musicValueDef = 1;
    public float musicValue = 1;
    public const float sfxValueDef = 1;
    public float sfxValue = 1;

    [Header("Input")]
    public const float InputDeadZoneDef = 10;
    public float InputDeadZone = 10;

    [Header("UnitMotion")]
    public const float moveSpeedDef = 250;
    public float moveSpeed = 250;
    public const float jumpForceDef = 250;
    public float jumpForce = 250;
    public const float horizontalMultiplyDef = 1.5f;
    public float horizontalMultiply = 1.5f;
    public const float downSpeedDef = 20;
    public float downSpeed = 20;

    [Header("AiLogic")]
    public const float deadzoneForMoveDef = 0.3f;
    public float deadzoneForMove = 0.3f;
    public const float deadzoneForJumpDef = 3;
    public float deadzoneForJump = 3;

    [Header("Ball")]
    public const float pushForceDef = 25;
    public float pushForce = 25f;
    public const float maxMagnetudeDef = 5;
    public float maxMagnetude = 5;


    public void SetDefaultValue()
    {
        musicValue = musicValueDef;
        sfxValue = sfxValueDef;
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
