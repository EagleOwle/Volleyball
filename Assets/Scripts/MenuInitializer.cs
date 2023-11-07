using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInitializer : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Restart), Time.deltaTime);
    }

    public void Restart()
    {
        UIHud.Instance.OnChangePanel(UIPanelName.Main);
    }

}
