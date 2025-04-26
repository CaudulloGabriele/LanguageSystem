using System;
using System.Linq;
using System.Collections.Generic;


/// <summary>
/// Static class with useful properties and methods to manage the game's languages
/// </summary>
public static class GameLanguages
{

    /// <summary>
    /// Array containing every language in the game
    /// </summary>
    private static readonly IEnumerable<Language> languages = Enum.GetValues(typeof(Language)).OfType<Language>().ToArray();
    /// <summary>
    /// <inheritdoc cref="languages" />
    /// </summary>
    public static IEnumerable<Language> Languages => languages;

    /// <summary>
    /// Array containing the names of every language in the game
    /// </summary>
    private static readonly IEnumerable<string> languagesNames = languages.Select(language => language.ToString()).ToArray();
    /// <summary>
    /// <inheritdoc cref="languagesNames" />
    /// </summary>
    public static IEnumerable<string> LanguagesNames => languagesNames;


    /// <summary>
    /// Returns the name of the desired language
    /// </summary>
    /// <param name="language">Language to get the name of</param>
    /// <returns></returns>
    public static string GetNameOfLanguage(Language language)
        => languagesNames.ElementAt((int)language);

}


/// <summary>
/// Enum defining all the languages present in the game
/// </summary>
public enum Language
{
    English, //(CAN BE REMOVED IF NOT NEEDED IN YOUR GAME)
    Italiano, //(CAN BE REMOVED IF NOT NEEDED IN YOUR GAME)
    //ADD HERE ANY OTHER LANGUAGE NEEDED IN THE GAME
}