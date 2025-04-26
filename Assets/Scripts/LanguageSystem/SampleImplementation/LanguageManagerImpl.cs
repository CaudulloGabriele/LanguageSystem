using UnityEngine;

/// <summary>
/// Class defining a basic implementation of the BaseLanguageManager, whose data for saved language is managed using PlayerPrefs
/// </summary>
public class LanguageManagerImpl
    : BaseLanguageManager
{

    /// <summary>
    /// Key used by the PlayerPrefs to store the saved value for the game's language
    /// </summary>
    private const string SAVED_LANGUAGE_VALUE_NAME = "savedLanguage";


    public override Language GetSavedLanguage()
        => (Language)PlayerPrefs.GetInt(SAVED_LANGUAGE_VALUE_NAME);

    public override void SaveCurrentLanguage(Language currentLanguage)
        => PlayerPrefs.SetInt(SAVED_LANGUAGE_VALUE_NAME, (int)currentLanguage);
}