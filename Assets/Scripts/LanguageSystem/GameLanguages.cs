using System;
using System.Linq;
using System.Collections.Generic;

public static class GameLanguages
{

    private static readonly IEnumerable<Language> languages = Enum.GetValues(typeof(Language)).OfType<Language>();
    public static IEnumerable<Language> Languages => languages;

    private static readonly IEnumerable<string> languagesNames = languages.Select(language => language.ToString()).ToArray();
    public static IEnumerable<string> LanguagesNames => languagesNames;


    public static string GetNameOfLanguage(Language language)
        => languagesNames.ElementAt((int)language);
}


public enum Language
{
    English,
    Italiano,
}