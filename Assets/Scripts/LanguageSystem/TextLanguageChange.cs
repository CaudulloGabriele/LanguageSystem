using TMPro;
using UnityEngine;

/// <summary>
/// Manages how a text changes its language, based on the current language in LanguageManager
/// </summary>
public class TextLanguageChange : MonoBehaviour
{

    #region Variables

    [Tooltip("Reference to tha only instance of the LanguageManager")]
    private static LanguageManager lm;
    private static LanguageManager RefLanguageManager
    {
        get
        {
            //if the static reference to the LanguageManager instance is still null, gets it
            if (!lm) { lm = LanguageManager.Instance; }

            return lm;
        }

        set { lm = value; }

    }


    [Header("Text Change")]

    [Tooltip("Reference to the text to change")]
    [SerializeField]
    private TextMeshProUGUI textToChange;


    [Tooltip("Text to show in all languages")]
    [SerializeField]
    private MultiLanguageString textAllLang;

    #endregion

    #region MonoBehaviour Methods

    private void OnValidate()
    {
        //validates the MultiLanguageString of this text
        if (LanguageManager.InitializationComplete) { textAllLang.OnValidate();}

    }

    private void Start()
    {
        //adds this text in the list of the LanguageManager
        RefLanguageManager.AddTextToList(this);

        //then, changes own language based on the current language
        ChangeTextLanguage(RefLanguageManager.GetCurrentLanguage());

    }

    #endregion

    #region Text Language Managment

    /// <summary>
    /// Changes language based on the received value
    /// 0 - ENGLISH
    /// 1 - ITALIAN
    /// </summary>
    /// <param name="newLanguage"></param>
    public void ChangeTextLanguage(int newLanguage) { textToChange.text = textAllLang.GetString(newLanguage); }
    /// <summary>
    /// Allows to change the text to show for this text in all languages
    /// </summary>
    /// <param name="newText"></param>
    public void UpdateTextAllLang(MultiLanguageString newText)
    {
        textAllLang = newText;
        //then, changes the text currently shown on this text based on the current language
        ChangeTextLanguage(RefLanguageManager.GetCurrentLanguage());

    }
    /// <summary>
    /// Allows to change the text to show for this text in all languages
    /// </summary>
    /// <param name="newEnglishText"></param>
    /// <param name="newItalianText"></param>
    public void UpdateTextAllLang(string[] str)
    {
        //updates the text with the new values
        MultiLanguageString newText = new(str);
        UpdateTextAllLang(newText);

    }

    /// <summary>
    /// Adds a string at the end of each language text
    /// </summary>
    /// <param name="toAddAllLang"></param>
    public void AddToTextAllLang(MultiLanguageString toAddAllLang)
    {
        MultiLanguageString str = (textAllLang + toAddAllLang);
        UpdateTextAllLang(str);
    }
    /// <summary>
    /// Adds a string at the end of each language text
    /// </summary>
    /// <param name="englishToAdd"></param>
    /// <param name="italianToAdd"></param>
    public void AddToTextAllLang(string[] str) { AddToTextAllLang(str); }
    /// <summary>
    /// Allows to add a string at the end of a single language text
    /// </summary>
    /// <param name="toAdd"></param>
    /// <param name="language"></param>
    public void AddToText(string toAdd, int language)
    {
        MultiLanguageString toAddAllLang = new(language, toAdd);
        AddToTextAllLang(toAddAllLang);

    }

    #endregion

    #region Getter Methods

    /// <summary>
    /// Returns this text in the currently set game's language
    /// </summary>
    /// <returns></returns>
    public string GetCurrentLanguageText() { return textAllLang.GetString(RefLanguageManager.GetCurrentLanguage()); }

    /// <summary>
    /// Returns this text in english
    /// </summary>
    /// <returns></returns>
    public string GetEnglishText() { return textAllLang.GetString(0); }
    /// <summary>
    /// Returns this text in italian
    /// </summary>
    /// <returns></returns>
    public string GetItalianText() { return textAllLang.GetString(1); }

    #endregion

}