using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class PlayerPreference
{
    public void Initialise(int playerId)
    {
        LoadPlayerPrefs(playerId);
    }

    public void SavePlayerName(string name, int playerId)
    {
        PlayerPrefs.SetString("PlayerName" + playerId, name);
        this.name = name;
    }
    public string Name => name;
    [SerializeField] private string name;

    [Space()]
    public InputType inputType = InputType.button;
    [Space()]
    public PlayerInput leftKey;
    public PlayerInput rightKey;
    public PlayerInput jumpKey;
   
    private void LoadPlayerPrefs(int playerId)
    {
        if (PlayerPrefs.HasKey("PlayerName" + playerId))
        {
            name = PlayerPrefs.GetString("PlayerName" + playerId);
        }

        if (PlayerPrefs.HasKey("MouseMoveSence" + playerId))
        {
            mouseMoveSence = PlayerPrefs.GetFloat("MouseMoveSence" + playerId);
        }

        if (PlayerPrefs.HasKey("MouseJumpSence" + playerId))
        {
            mouseJumpSence = PlayerPrefs.GetFloat("MouseJumpSence" + playerId);
        }

        if (PlayerPrefs.HasKey("JoysticDeadZone" + playerId))
        {
            mouseJumpSence = PlayerPrefs.GetFloat("JoysticDeadZone" + playerId);
        }

        leftKey.ReadKey();
        rightKey.ReadKey();
        jumpKey.ReadKey();
    }

    public void SaveMouseMoveSence(float value, int playerId)
    {
        PlayerPrefs.SetFloat("MouseMoveSence" + playerId, value);
        mouseMoveSence = value;
    }
    public float MouseMoveSence => mouseMoveSence;
    private float mouseMoveSence = 1;

    public void SaveMouseJumpSence(float value, int playerId)
    {
        PlayerPrefs.SetFloat("MouseJumpSence" + playerId, value);
        mouseJumpSence = value;
    }
    public float MouseJumpSence => mouseJumpSence;
    private float mouseJumpSence = 10;

    public void SaveJoysticDeadZone(float value, int playerId)
    {
        PlayerPrefs.SetFloat("JoysticDeadZone" + playerId, value);
        joysticDeadZone = value;
    }
    public float JoysticDeadZone => joysticDeadZone;
    private float joysticDeadZone = 10;

    [Space()]
    public LocalizedString nameLocalizedString;
}

[System.Serializable]
public class PlayerInput
{
    public ActionType type;
    public int playerIndex;

    public void ReadKey()
    {
        if (PlayerPrefs.HasKey(type.ToString() + playerIndex))
        {
            key = (KeyCode)PlayerPrefs.GetInt(type.ToString() + playerIndex);
        }
    }

    public void WriteKey(KeyCode key)
    {
        PlayerPrefs.SetInt(type.ToString() + playerIndex, (int)key);
        this.key = key;
    }

    public KeyCode Key => key;
    [SerializeField] private KeyCode key;

}

[System.Serializable]
public enum ActionType
{
    Left,
    Right,
    Jump
}