using TMPro;

public class TextLanguageChanger
    : BaseLanguageChanger<TMP_Text, string>
{
    protected override void ManageObjectChangeOnLanguageChange(string newValue)
        => objToChange.text = newValue;

    protected override string GetDefaultValueForLanguage(Language language)
        => $"New Text for '{GameLanguages.GetNameOfLanguage(language)}'";
}