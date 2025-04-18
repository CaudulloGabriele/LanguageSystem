using TMPro;

public class TextLanguageChangeReact
    : LanguageChangeReact<TMP_Text, string>
{
    protected override void ManageObjectChangeOnLanguageChange(string newValue)
        => objToChange.text = newValue;

    protected override string GetDefaultValueForLanguage(Language language)
        => $"New Text for '{GameLanguages.GetNameOfLanguage(language)}'";


    public void AddToTextForLanguage(Language language, string toAdd, int? insertAt = null)
    {
        string previousValue = GetValueForLanguage(language);
        SetValueForLanguage(language, previousValue.Insert((insertAt ?? previousValue.Length), toAdd));
    }
}