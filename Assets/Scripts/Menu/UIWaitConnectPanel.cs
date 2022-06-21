using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaitConnectPanel : UIPanel
{
    [SerializeField] private Button cancelBtn;

    public override void Init()
    {
        base.Init();
        cancelBtn.onClick.AddListener(OnButtonCancel);
    }

    private void OnButtonCancel()
    {
        
    }

}
