using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class defining how an object that has to change the game's current language manages how to change the language correctly
/// </summary>
/// <typeparam name="TObject">Type of the MonoBehaviour object whose behaviour will change the game's current language</typeparam>
/// <typeparam name="TValue">Type of the value used by the object whose behaviour will change the game's current language</typeparam>
public abstract class LanguageChanger<TObject, TValue>
    : LanguageValuesHandler<TValue>
    where TObject : MonoBehaviour
{

    #region Variables

    [Tooltip("Reference to the object whose behaviour will change the game's current language")]
    [SerializeField]
    protected TObject objChanger;

    [Tooltip("If true, the object has correctly been initialized and will be allowed to change the game's current language")]
    private bool initialized;

    #endregion


    protected override void OnEnable()
    {
        base.OnEnable();


        //initializes the object using the current language
        Language currentLanguage = BaseLanguageManager.GetCurrentLanguage();
        InitializeObject(currentLanguage, GameLanguages.Languages);

        UpdateObject(currentLanguage);
        initialized = true;
    }


    #region LanguageChangeListener Methods

    protected override void DoOnLanguageChanged(Language language)
        => UpdateObject(language); //updates the object everytime the current language changes

    #endregion


    #region Object Management

    /// <summary>
    /// Initializes the object whose behaviour will change the game's current language
    /// </summary>
    /// <param name="currentLanguage">Current language of the game</param>
    /// <param name="allLanguages">Array containing all the languages present in the game</param>
    protected abstract void InitializeObject(Language currentLanguage, IEnumerable<Language> allLanguages);

    /// <summary>
    /// Updates the object whose behaviour will change the game's current language
    /// </summary>
    /// <param name="language"></param>
    protected abstract void UpdateObject(Language language);

    /// <summary>
    /// Returns the current value of the object whose behaviour will change the game's current language
    /// </summary>
    /// <returns></returns>
    protected abstract TValue GetObjectCurrentValue();

    #endregion


    #region Language Changing Management

    /// <summary>
    /// Changes the game's current language based on the current value of the object
    /// </summary>
    protected void ChangeCurrentLanguageToObjectCurrentValue()
    {
        //if the object is not initialized or this method was called due to the reaction of the current language changing, returns
        if (!initialized || reactingToLanguageChange) { return; }
        //...otherwise...

        //...changes the current language based on the current value of the object
        BaseLanguageManager.ChangeLanguage(languageValues.GetSingleWithValue(GetObjectCurrentValue()).Language);
    }

    #endregion

}