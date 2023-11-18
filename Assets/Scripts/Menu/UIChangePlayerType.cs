using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangePlayerType : MonoBehaviour
{
    [SerializeField] private DropdownOptionPlayerType dropdownOptionPlayerType;
    [SerializeField] private Dropdown dropdown;

    private ScenePreference.Scene currentScene;
    private int playerIndex;

    public void SetCurrentScene(ScenePreference.Scene currentScene, int playerIndex)
    {
        this.currentScene = currentScene;
        this.playerIndex = playerIndex;
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.value = (int)currentScene.playersType[playerIndex];
        dropdown.onValueChanged.AddListener(OnChangePlayerDropdownValue);
        dropdownOptionPlayerType.ChangeCurrentScene();

    }

    private void OnChangePlayerDropdownValue(int value)
    {
        currentScene.playersType[playerIndex] = (PlayerType)value;
    }

}
