using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPreference
{
    public string Name;
    public float InputDeadZone = 10;
    public InputType inputType = InputType.button;
}
