using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPreferenceMenu : UIPanel
{
    [SerializeField] private Button graphicsMenuBtn;
    [SerializeField] private Button soundMenuBtn;
    [SerializeField] private Button gameMenuBtn;
    [SerializeField] private Button returnBtn;

    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject preferencePanel;

    public override void Init()
    {
        base.Init();
        graphicsMenuBtn.onClick.AddListener(OnButtonGraphics);
        soundMenuBtn.onClick.AddListener(OnButtonSound);
        gameMenuBtn.onClick.AddListener(OnButtonGame);
        returnBtn.onClick.AddListener(OnButtonReturn);

        ShowButtonPanel();
    }

    public void ShowButtonPanel()
    {
        buttonPanel.SetActive(true);
        preferencePanel.SetActive(false);
    }

    private void ShowPreferencePanel()
    {
        buttonPanel.SetActive(false);
        preferencePanel.SetActive(true);
    }

    private void OnButtonGraphics()
    {

    }

    private void OnButtonSound()
    {
        
    }

    private void OnButtonGame()
    {
        ShowPreferencePanel();
    }

    private void OnButtonReturn()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Main);
    }

}
