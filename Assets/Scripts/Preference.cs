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
    public float musicValue = 1;
    public float sfxValue = 1;

    [Header("Input")]
    public float InputDeadZone = 10;

    [Header("UnitMotion")]
    public float moveSpeed = 250;
    public float jumpForce = 250;
    public float horizontalMultiply = 1.5f;
    public float downSpeed = 20;

    [Header("AiLogic")]
    public float deadzoneForMove = 0.3f;
    public float deadzoneForJump = 3;

    [Header("Ball")]
    public float pushForce = 25f;
    public float maxMagnetude = 5;

}
