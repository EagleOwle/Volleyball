using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalSelector : MonoBehaviour
{
    private bool _active = false;
    private static LocalSelector _instance;
    public static LocalSelector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LocalSelector>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("LocalSelector");
                    _instance = singletonObject.AddComponent<LocalSelector>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    public void ChangeLocale(int localeId)
    {
        if (_active == true)
        {
            return;
        }

        //Debug.Log("Local id= " + localeId);
        StartCoroutine(SetLocale(localeId));
    }

    IEnumerator SetLocale(int _localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        Preference.Singleton.language = (Language)_localeID;
        _active = false;
    }
}
