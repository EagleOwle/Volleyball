using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePreference", menuName = "GamePreference")]
public class Preference : ScriptableObject
{
    private static Preference singletone;
    public static Preference Singletone
    {
        get
        {
            if(singletone == null)
            {
                singletone = Resources.Load("Preference") as Preference;
            }

            return singletone;
        }
    }

    [Header("Sound")]
    public int musicValue = 1;
    public int sfxValue = 1;

    [Header("Input")]
    public int deadZone = 10;

    [Header("UnitMotion")]
    public int moveSpeed = 250;
    public int jumpForce = 250;
    public float horizontalMultiply = 1.5f;
    public float downSpeed = 20;

    [Header("AiLogic")]
    public float deadzoneForMove = 0.3f;
    public float deadzoneForJump = 3;

    [Header("Ball")]
    public float pushForce = 25f;
    public float maxMagnetude = 5;

}
