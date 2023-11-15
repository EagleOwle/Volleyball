using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    //private bool _active = false;
    //public static LanguageSelector Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = FindObjectOfType<LanguageSelector>();

    //            if (_instance == null)
    //            {
    //                GameObject singletonObject = new GameObject("LanguageSelector");
    //                _instance = singletonObject.AddComponent<LanguageSelector>();
    //                GameObject.DontDestroyOnLoad(_instance);
    //            }
    //        }


    //        return _instance;
    //    }
    //}
    //private static LanguageSelector _instance;

    [SerializeField] private Button rusButton, engButton, zhButton;

    private void Awake()
    {
        rusButton.onClick.AddListener(() => ChangeLocale(2));
        engButton.onClick.AddListener(() => ChangeLocale(1));
        zhButton.onClick.AddListener(() => ChangeLocale(0));
    }

    public void ChangeLocale(int localeId)
    {
        StopAllCoroutines();
        StartCoroutine(SetLocale(localeId));
    }

    private IEnumerator SetLocale(int _localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        Preference.Singleton.language = (Language)_localeID;
    }

}
