using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPreferenceMenu : UIPanel
{
    [SerializeField] private Button graphicsMenuBtn;
    [SerializeField] private Button soundMenuBtn;
    [SerializeField] private Button returnBtn;

    public override void Init()
    {
        base.Init();
        graphicsMenuBtn.onClick.AddListener(OnButtonGraphics);
        soundMenuBtn.onClick.AddListener(OnButtonSound);
        returnBtn.onClick.AddListener(OnButtonReturn);
    }

    private void OnButtonGraphics()
    {

    }

    private void OnButtonSound()
    {

    }

    private void OnButtonReturn()
    {
        UIHud.Singletone.OnChangePanel(UIPanelName.Main);
    }

}
