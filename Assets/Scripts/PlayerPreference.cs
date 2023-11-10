using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPreference
{
    public string Name;
    [Space()]
    public InputType inputType = InputType.button;
    [Space()]
    public PlayerInput leftKey;
    public PlayerInput rightKey;
    public PlayerInput jumpKey;
    [Space()]
    public float mouseMoveSence = 1;
    public float mouseJumpSence = 10;
    [Space()]
    public float joysticDeadZone = 10;
}

[System.Serializable]
public class PlayerInput
{
    public ActionType type;
    public KeyCode key;
}

[System.Serializable]
public enum ActionType
{
    Left,
    Right,
    Jump
}