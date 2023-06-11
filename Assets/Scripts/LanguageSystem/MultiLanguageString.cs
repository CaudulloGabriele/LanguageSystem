using System;
using UnityEngine;

/// <summary>
/// Class defining a string with multiple languages
/// </summary>
[Serializable]
public class MultiLanguageString
{

    #region String Languages

    [Serializable]
    private class LanguageString
    {
        [HideInInspector]
        [Tooltip("The name of the language")]
        public string languageName = "Language";
        [Tooltip("The string to show when the game's language is set to this language")]
        public string langString = "String in 'Language'";

    }


    [Tooltip("Array containing the string to show in all the languages of the game")]
    [SerializeField]
    private LanguageString[] stringsAllLang;

    #endregion


    /// <summary>
    /// Checks if the language options have changed
    /// </summary>
    public void OnValidate()
    {
        //if the number of languages in the game changed, resizes the array of strings
        int nLanguages = GameLanguages.GetAmountOfLanguages();
        if (stringsAllLang == null) { stringsAllLang = new LanguageString[nLanguages]; }
        if (stringsAllLang.Length != nLanguages) { Array.Resize(ref stringsAllLang, nLanguages); }
        //updates the array of strings in all languages based on the number of languages in the game
        string[] languages = GameLanguages.GetLanguageOptions();
        for (int i = 0; i < nLanguages; ++i)
        {
            string languageName = languages[i];
            //if the looped LanguageString is null, it is initialized
            if (stringsAllLang[i] == null)
            {
                stringsAllLang[i] = new() { langString = ("String in '" + languageName + "'") };
            }

            //updates the language name of the looped LanguageString
            stringsAllLang[i].languageName = languageName;

        }

    }


    /// <summary>
    /// Constructor method for a multi-language string
    /// </summary>
    /// <param name="strAllLang"></param>
    public MultiLanguageString(string[] strAllLang)
    {
        int nLanguages = GameLanguages.GetAmountOfLanguages();
        for (int i = 0; i < nLanguages; ++i) { SetString(i, strAllLang[i]); }

    }
    /// <summary>
    /// Constructor methods for a multi-language string, to set only one language
    /// </summary>
    /// <param name="languageIndex"></param>
    /// <param name="str"></param>
    public MultiLanguageString(int languageIndex, string str) { SetString(languageIndex, str); }


    #region Getter Methods

    /// <summary>
    /// Returns the string of the desired language
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public string GetString(int language)
    {
        string str;

        if (!ReceivedWronLanguageIndex(language)) { str = stringsAllLang[language].langString; }
        else
        {
            str = "error";
            Debug.LogError("ERROR WHEN TRYING TO RECEIVE MULTI-LANGUAGE STRING. LANGUAGE RECEIVED: " + language);
        }

        return str;
    }

    /// <summary>
    /// Returns the length of the string of the desired language
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    public int GetLength(int language, out string str)
    {
        str = GetString(language);
        return str.Length;
    }

    #endregion

    #region Setter Methods

    /// <summary>
    /// Allows to set the string in the desired language
    /// </summary>
    /// <param name="language"></param>
    /// <param name="str"></param>
    public void SetString(int language, string str)
    {
        //Debug.Log("INDEX: " + language + " || ARRAY: " + stringsAllLang + " || NULL ? " + (stringsAllLang == null));
        if (stringsAllLang == null) { return; }
        if (!ReceivedWronLanguageIndex(language)) { stringsAllLang[language].langString = str; }

        else { Debug.LogError("ERROR WHEN TRYING TO SET MULTI-LANGUAGE STRING. LANGUAGE RECEIVED: " + language); }

    }

    /// <summary>
    /// Manages how two MultiLanguageStrings can be added
    /// </summary>
    /// <param name="str"></param>
    /// <param name="toAdd"></param>
    /// <returns></returns>
    public static MultiLanguageString operator +(MultiLanguageString str, MultiLanguageString toAdd)
    {
        int nLanguages = GameLanguages.GetAmountOfLanguages();
        for (int i = 0; i < nLanguages; ++i) { str.stringsAllLang[i].langString += toAdd.stringsAllLang[i].langString; }

        return str;
    }

    /// <summary>
    /// Returns true if the received language index is indicating a non-existent language
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    private bool ReceivedWronLanguageIndex(int language) { return ((language < 0) || (language >= stringsAllLang.Length)); }

    #endregion

}
