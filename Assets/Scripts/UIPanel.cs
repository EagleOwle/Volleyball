using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPanelName { Pause, Game, Timer }

public abstract class UIPanel : MonoBehaviour
{
    public UIPanelName panelName;

    protected virtual void ChangePanel(UIPanelName panelName)
    {
        if (panelName == this.panelName)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Init()
    {
        UIHud.ActionChangePanel += ChangePanel;
        gameObject.SetActive(false);
    }

    protected void OnDestroy()
    {
        UIHud.ActionChangePanel -= ChangePanel;
    }
}
