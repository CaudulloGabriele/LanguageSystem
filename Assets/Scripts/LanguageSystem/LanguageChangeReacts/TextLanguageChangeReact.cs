using TMPro;
using UnityEngine;


/// <summary>
/// Class defining a text that has to change the displayed text based on the current language
/// </summary>
public class TextLanguageChangeReact
    : LanguageChangeReact<TMP_Text, string>
{

    protected override void ManageObjectChangeOnLanguageChange(string newValue)
        => objToChange.text = newValue;

    protected override string GetDefaultValueForLanguage(Language language)
        => $"New Text for '{GameLanguages.GetNameOfLanguage(language)}'";


    #region AddToText Methods

    /// <summary>
    /// Allows to add/insert a string to the text for the desired language
    /// </summary>
    /// <param name="language">Language for which the value has to add the received 'toAdd' parameter</param>
    /// <param name="toAdd">String to add/insert to the text</param>
    /// <param name="insertAt">
    /// When not null, the received string will be inserted at the specified position.
    /// Otherwise, the received string will be added at the end of the current text.
    /// </param>
    public void AddToTextForLanguage(Language language, string toAdd, uint? insertAt = null)
    {
        string previousValue = GetValueForLanguage(language);
        SetValueForLanguage(language, previousValue.Insert(((int?)insertAt ?? previousValue.Length), toAdd));
    }

    /// <summary>
    /// Allows to add/insert a string to the text for every language
    /// </summary>
    /// <param name="toAdd">String to add/insert to the text</param>
    /// <param name="insertAt">
    /// When not null, the received string will be inserted at the specified position.
    /// Otherwise, the received string will be added at the end of the current text.
    /// </param>
    public void AddToTextForAllLanguages(string toAdd, uint? insertAt = null)
    {
        foreach (Language language in GameLanguages.Languages) { AddToTextForLanguage(language, toAdd, insertAt); }
    }

    #endregion

    #region Text Appearance Management

    /// <summary>
    /// Allows to change the text's color
    /// </summary>
    /// <param name="newColor"></param>
    public void ChangeTextColor(Color newColor)
        => objToChange.color = newColor;

    /// <summary>
    /// Allows to change text's transparency(the value MUST be a value between 0 and 1)
    /// </summary>
    /// <param name="newAlpha"></param>
    public void ChangeTextTransparency(float newAlpha)
        => objToChange.alpha = newAlpha;

    #endregion

}