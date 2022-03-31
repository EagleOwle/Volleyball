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

    [SerializeField] private UIPanel[] panels;

    public void ChangePanel(string namePanel)
    {
        foreach (var item in panels)
        {
            if(item.namePanel == namePanel)
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

}
