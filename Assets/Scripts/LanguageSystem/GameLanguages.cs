using System;
using System.Linq;
using System.Collections.Generic;

public static class GameLanguages
{

    private static readonly IEnumerable<Language> languages = Enum.GetValues(typeof(Language)).OfType<Language>();

    private static readonly IEnumerable<string> languagesNames = languages.Select(language => language.ToString()).ToArray();


    public static IEnumerable<Language> GetLanguages()
        => languages;

    public static IEnumerable<string> GetLanguagesNames()
        => languagesNames;

    public static string GetNameOfLanguage(Language language)
        => languagesNames.ElementAt((int)language);
}


public enum Language
{
    English,
    Italiano,
}