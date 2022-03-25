using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _pauseBtn;

    private void Start()
    {
        _restartBtn.onClick.AddListener(OnButtonRestart);
        _pauseBtn.onClick.AddListener(OnButtonPause);
    }

    private void OnButtonRestart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnButtonPause()
    {
        Game.Instance.RechangeStatus();
    }

}
