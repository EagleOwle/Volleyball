using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPanelName { Pause, Game, Timer, Main, Preference, ScenePreference, Connect }

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
        UIHud.Instance.ActionChangePanel += ChangePanel;
        //Debug.LogError("Init " + panelName);
    }

    protected virtual void OnDestroy()
    {
        if (UIHud.Instance != null)
        {
            UIHud.Instance.ActionChangePanel -= ChangePanel;
        }
    }
}
