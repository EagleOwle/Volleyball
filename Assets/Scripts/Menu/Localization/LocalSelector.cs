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

    public int CurrentLocaleId { get; private set; } // Публичное свойство для доступа к localeId

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        //int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        //ChangeLocale(ID);
    }

    public void ChangeLocale(int localeId)
    {
        if (_active == true)
        {
            return;
        }

        StartCoroutine(SetLocale(localeId));
    }

    IEnumerator SetLocale(int _localeID)
    {
        _active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        CurrentLocaleId = _localeID; // Устанавливаем значение localeId
        _active = false;
    }
}
