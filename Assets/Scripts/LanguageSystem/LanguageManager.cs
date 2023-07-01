using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages how the language changes
/// </summary>
public class LanguageManager : MonoBehaviour
{

    [Tooltip("Indicates how many languages are present in the game")]
    public const int COUNT_LANGUAGES = 2;


    #region Variables

    [Tooltip("Only instance of the LanguageManager")]
    private static LanguageManager instance;
    /// <summary>
    /// Only instance of the LanguageManager
    /// </summary>
    public static LanguageManager Instance
    {
        get
        {
            //if no instance is set, searches for it in the scene
            if (!instance) { instance = FindObjectOfType<LanguageManager>(); }

            return instance;
        }
        
        private set { instance = value; }
    
    }


    [Tooltip("List of all the texts that need to change their texts when the language is changed")]
    private readonly List<TextLanguageChange> textsToChangeLanguage = new();

    [Tooltip("Defines all the languages of the game")]
    [SerializeField]
    private GameLanguages gameLanguages;

    [Tooltip("Reference to the dropdown list for changing language")]
	[SerializeField]
	private TMP_Dropdown[] languageDropdownLists = new TMP_Dropdown[1];


    [Tooltip("Indicates the current language:\n 0 = ENGLISH\n 1 = ITALIAN")]
	private int currentLanguage;


    /// <summary>
    /// (EDITOR ONLY)Indicates whether or not the LanguageManager's initialization is complete
    /// </summary>
    public static bool InitializationComplete { get; private set; }

	#endregion

	#region MonoBehaviour Methods

	/// <summary>
	/// Used as OnValidate, because the actual 'OnValidate' doesn't check for changes in values of the dropdown list
	/// </summary>
	private void OnDrawGizmos/*OnValidate*/()
	{
		//updates the options of the dropdown list for changing languages
		foreach (TMP_Dropdown dropdown in languageDropdownLists)
		{
            int nLanguages = gameLanguages.GetAmountOfLanguages();
            string[] languageOptions = gameLanguages.GetLanguageOptions();

            if (dropdown)
            {
                if (dropdown.options.Count != nLanguages)
                {
                    TMP_Dropdown.OptionData[] copy = dropdown.options.ToArray();
                    Array.Resize(ref copy, nLanguages);
                    dropdown.options = copy.ToList();
                }
                for (int i = 0; i < nLanguages; ++i)
                {
                    if (dropdown.options[i] == null) { dropdown.options[i] = new(); }
                    dropdown.options[i].text = languageOptions[i];

                }

            }

        }


        //comunicates the initialization of the LanguageManager has been completed
        InitializationComplete = true;

	}

	private void Awake()
    {
        //Singleton pattern
        if (!Instance) { Instance = this; }
        else if (Instance != this) { Destroy(this); return; }

    }

    private void Start()
	{
		//if there is the reference to the language dropdown list...
		foreach (TMP_Dropdown dropdown in languageDropdownLists)
		{
            if (dropdown)
            {
                //...initializes the dropdown list for changing language
                dropdown.onValueChanged.AddListener(LanguageChange);
                //...changes its value based on the saved language
                dropdown.value = currentLanguage;

            }

        }

        //Debug.Log("Saved language: " + settingsManager.savedLanguage);
    }

    #endregion


    #region Language Managment

    /// <summary>
    /// Changes all the texts in the list based on the received language. Called whenever the dropdown list for the language changes its value
    /// </summary>
    public void LanguageChange(int language)
	{
		//updates the current language
		currentLanguage = language;

		//changes the language of each text in the list
		for (int i = 0; i < textsToChangeLanguage.Count; i++)
        {
			textsToChangeLanguage[i].ChangeTextLanguage(language);

			//Debug.Log("Changed language of: " + textsToChangeLanguage[i].name);
		}

        //Debug.Log("Language changed in: " + language);
    }
	/// <summary>
	/// Returns the currently set language
	/// </summary>
	/// <returns></returns>
	public int GetCurrentLanguage() { return currentLanguage; }
	/// <summary>
	/// Adds the received text to the list of text to change language of, unless the text is already in the list
	/// </summary>
	/// <param name="newText"></param>
	public void AddTextToList(TextLanguageChange newText)
	{
		if (textsToChangeLanguage.Contains(newText)) return;

		textsToChangeLanguage.Add(newText);
	
	}

    #endregion


    /// <summary>
    /// Returns the number of languages present in the game
    /// </summary>
    /// <returns></returns>
    public static int GetAmountOfLanguages() { return Instance.gameLanguages.GetAmountOfLanguages(); }
    /// <summary>
    /// Returns an array containing the names of all the game's language options
    /// </summary>
    /// <returns></returns>
    public static string[] GetLanguageOptions() { return Instance.gameLanguages.GetLanguageOptions(); }

}
