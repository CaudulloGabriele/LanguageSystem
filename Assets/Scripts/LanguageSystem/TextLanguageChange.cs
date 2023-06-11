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
    private static LanguageManager refLanguageManager
    {
        get
        {
            //if the static reference to the LanguageManager instance is still null, gets it
            if (!lm) { lm = LanguageManager.inst; }

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


    /*
    [Header("No-Text Button Change")]

    [Tooltip("Indicates if the thing to change language of is actually a button with no text (in which case, only the image is changed)")]
    [SerializeField]
    private bool isNoTextButton;

    [Tooltip("Reference to the button to change image of")]
    [SerializeField]
    private Image buttonImageToChange = default;


    [Tooltip("Reference to the sprite to set as image of the button when language is set to english")]
    [SerializeField]
    private Sprite englishButton;
    [Tooltip("Reference to the sprite to set as image of the button when language is set to italian")]
    [SerializeField]
    private Sprite italianButton;
    */

    #endregion

    #region MonoBehaviour Methods

    private void OnValidate()
    {
        //validates the MultiLanguageString of this text
        textAllLang.OnValidate();

    }

    private void Awake()
    {
        /*
        //if a text has to be changed and no reference is set, automatically gets it
        if (!isNoTextButton && textToChange == null) { textToChange = GetComponent<TextMeshProUGUI>(); }
        //otherwise, if a button has to be changed and no reference is set, automatically gets it
        else if (isNoTextButton && buttonImageToChange == null) { buttonImageToChange = GetComponent<Image>(); }
        */
    }

    private void Start()
    {
        //adds this text in the list of the LanguageManager
        refLanguageManager.AddTextToList(this);

        //then, changes own language based on the current language
        ChangeTextLanguage(lm.GetCurrentLanguage());

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
        ChangeTextLanguage(refLanguageManager.GetCurrentLanguage());

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


    /// <summary>
    /// Allows to change the color of this MultiLanguage-Text
    /// </summary>
    /// <param name="newColor"></param>
    public void ChangeTextColor(Color newColor) { textToChange.color = newColor; }


    #region Getter Methods

    /// <summary>
    /// Returns this english text
    /// </summary>
    /// <returns></returns>
    public string GetEnglishText() { return textAllLang.GetString(0); }
    /// <summary>
    /// Returns this italian text
    /// </summary>
    /// <returns></returns>
    public string GetItalianText() { return textAllLang.GetString(1); }

    #endregion

}