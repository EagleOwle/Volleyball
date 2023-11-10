using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPreference
{
    public string Name;
    public float InputDeadZone = 10;
    public InputType inputType = InputType.button;
    public PlayerInput leftKey;
    public PlayerInput rightKey;
    public PlayerInput jumpKey;

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