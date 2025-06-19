using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    private const string LanguagePrefKey = "selectedLanguage";

    private void Start()
    {
        // «читуЇмо збережену мову при старт≥
        string savedLangCode = PlayerPrefs.GetString(LanguagePrefKey, "uk");
        SetLocaleByCode(savedLangCode);
    }

    public void SetUkrainian()
    {
        SetLocaleByCode("uk");
    }

    public void SetEnglish()
    {
        SetLocaleByCode("en-US");
    }

    private void SetLocaleByCode(string code)
    {
        var availableLocales = LocalizationSettings.AvailableLocales.Locales;

        foreach (var locale in availableLocales)
        {
            if (locale.Identifier.Code == code)
            {
                LocalizationSettings.SelectedLocale = locale;
                PlayerPrefs.SetString(LanguagePrefKey, code);
                PlayerPrefs.Save();
                break;
            }
        }
    }
}