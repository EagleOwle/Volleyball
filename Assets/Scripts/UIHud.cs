using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    private static UIHud _instance;
    public static UIHud Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIHud>();
            }

            return _instance;
        }
    }

    [SerializeField] private Button _restartBtn;
    [SerializeField] private Button _pauseBtn;

    [Space()]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject timerPanel;

    private void Start()
    {
        _restartBtn.onClick.AddListener(OnButtonRestart);
        _pauseBtn.onClick.AddListener(OnButtonPause);

        ShowTimerPanel();
    }

    private void OnButtonRestart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnButtonPause()
    {
        if (Game.Instance.status == Game.Status.game)
        {
            Game.Instance.SetStatus(Game.Status.pause);
            ShowPausePanel();
        }
        else
        {
            Game.Instance.SetStatus(Game.Status.game);
            HidePausePanel();
        }
    }

    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ShowTimerPanel()
    {
        timerPanel.SetActive(true);
    }

}
