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
    public static LanguageManager inst;


    [Tooltip("List of all the texts that need to change their texts when the language is changed")]
    private readonly List<TextLanguageChange> textsToChangeLanguage = new();

    [Tooltip("Reference to the dropdown list for changing language")]
	[SerializeField]
	private TMP_Dropdown[] languageDropdownLists = new TMP_Dropdown[1];


    [Tooltip("Indicates the current language:\n 0 = ENGLISH\n 1 = ITALIAN")]
	private int currentLanguage;

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
            int nLanguages = GameLanguages.GetAmountOfLanguages();
            string[] languageOptions = GameLanguages.GetLanguageOptions();

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

	}

	private void Awake()
    {
		//Singleton pattern
		if (inst) { Destroy(gameObject); return; }
		else { inst = this; }

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

        //Debugging.ShowLog(("Saved language: " + settingsManager.savedLanguage), 0);
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

			//Debugging.ShowLog(("Changed language of: " + textsToChangeLanguage[i].name), 0);
		}

        //Debugging.ShowLog(("Language changed in: " + language), 0);
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

}
