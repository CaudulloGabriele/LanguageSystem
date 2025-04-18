using UnityEngine;

public class LanguageManagerImpl
    : BaseLanguageManager
{

    private const string SAVED_LANGUAGE_VALUE_NAME = "savedLanguage";


    public override Language GetSavedLanguage()
        => (Language)PlayerPrefs.GetInt(SAVED_LANGUAGE_VALUE_NAME);

    public override void SaveCurrentLanguage()
        => PlayerPrefs.SetInt(SAVED_LANGUAGE_VALUE_NAME, (int)GetCurrentLanguage());
}
