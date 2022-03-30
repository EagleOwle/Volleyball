using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : UIPanel
{
    [SerializeField] private Button _pauseBtn;

    private void OnEnable()
    {
        Game.Instance.SetStatus(Game.Status.game);
    }

    private void Start()
    {
        _pauseBtn.onClick.AddListener(OnButtonPause);
    }

    private void OnButtonPause()
    {
        UIHud.Instance.ChangePanel("PausePanel");
    }
}
