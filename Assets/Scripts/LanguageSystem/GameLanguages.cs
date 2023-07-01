using System;
using UnityEngine;

/// <summary>
/// Class defining the languages present in the game
/// </summary>
[Serializable]
public class GameLanguages
{
    /// <summary>
    /// Class defining a language in the game
    /// </summary>
    [Serializable]
    private class Language
    {
        [Tooltip("Indicates the name of the language, which will be used in the dropdown list and in the editor as name of this language")]
        public string thisOption;

        /// <summary>
        /// Constructor method for a language option
        /// </summary>
        /// <param name="option"></param>
        public Language(string option) { thisOption = option; }

    }


    [Tooltip("Array containing all the game's languages options")]
    [SerializeField]
    private Language[] languages =
    {
        new Language("English"),
        new Language("Italian"),

    };


    /// <summary>
    /// Returns the array of all the game's languages options
    /// </summary>
    /// <returns></returns>
    public string[] GetLanguageOptions()
    {
        string[] options = new string[languages.Length];
        for (int i = 0; i < languages.Length; ++i) { options[i] = languages[i].thisOption; }

        return options;

    }

    /// <summary>
    /// Returns the number of languages in the game
    /// </summary>
    /// <returns></returns>
    public int GetAmountOfLanguages() { return languages.Length; }

}
