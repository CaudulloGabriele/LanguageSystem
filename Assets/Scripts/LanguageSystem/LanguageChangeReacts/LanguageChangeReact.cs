using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using UnityEngine;

/// <summary>
/// Abstract class defining how an object that has to change based on the game's current language reacts to the game's language changing
/// </summary>
/// <typeparam name="TObject">Type of the object that will react to the game's current language changing</typeparam>
/// <typeparam name="TValue">Type of the value used by the object to change when the game's current language changes</typeparam>
public abstract class LanguageChangeReact<TObject, TValue>
    : LanguageValuesHandler<TValue>
    where TObject : MonoBehaviour
{

    [Tooltip("Reference to the object that will change when the game's current language changes")]
    [SerializeField]
    protected TObject objToChange;


    #region LanguageChangeListener Methods

    protected override void DoOnLanguageChanged(Language language)
        => ChangeObjectLanguageTo(language); //changes the object based on the language the game's current language changed to

    #endregion


    #region Language Change Management

    /// <summary>
    /// Allows to change the object based on the desired language
    /// </summary>
    /// <param name="language">Indicates the language to use to make this object change</param>
    public void ChangeObjectLanguageTo(Language language)
        => ManageObjectChangeOnLanguageChange(languageValues.Single(lv => (lv.Language == language)).Value);
    
    /// <summary>
    /// Defines how the object changes based on the new value received
    /// </summary>
    /// <param name="newValue">New value for the object</param>
    protected abstract void ManageObjectChangeOnLanguageChange(TValue newValue);

    #endregion

    #region Languages Value Management

    /// <summary>
    /// Returns the value used by this object to change for the desired language
    /// </summary>
    /// <param name="language">Language to get the value used by the object to change of</param>
    /// <returns></returns>
    protected TValue GetValueForLanguage(Language language)
        => languageValues.Single(lv => (lv.Language == language)).Value;

    /// <summary>
    /// Allows to set the value used by this object to change for the desired language
    /// </summary>
    /// <param name="language">Language the new value is for</param>
    /// <param name="newValue">New value for the language</param>
    /// <exception cref="Exception">
    /// Thrown when the value could not be set, either because the received language is invalid, unmanaged or an unexpected error
    /// (N.B.: the validation makes sure these exceptions won't be thrown, so they should never be seen in normal circumstances)
    /// </exception>
    public void SetValueForLanguage(Language language, TValue newValue)
    {
        //loops through every value for every language in the game...
        bool valueSetCorrectly = false;
        foreach (LanguageValue<TValue> loopedLanguageValue in languageValues)
        {
            //...and changes the value of the one used for the desired language
            if (loopedLanguageValue.Language == language)
            {
                loopedLanguageValue.Value = newValue;
                valueSetCorrectly = true;

                break;
            }
        }

        //if the value was not set correctly, an exception will be thrown explaining why the error happened
        if (!valueSetCorrectly)
        {
            string exceptionMessage = "THE VALUE WAS NOT SET CORRECTLY, BECAUSE ";
            if (!GameLanguages.Languages.Contains(language)) { exceptionMessage += $"THE RECEIVED LANGUAGE IS NOT MANAGED: {language}"; }
            else if (languageValues.All(lv => (lv.Language != language)))
            {
                exceptionMessage += $"THE ARRAY OF VALUES FOR LANGUAGES DOES NOT CONTAIN A VALUE FOR THIS LANGUAGE: {language}";
            }
            else { exceptionMessage += "OF AN UNEXPECTED ERROR!"; }

            throw new Exception(exceptionMessage);
        }
    }

    /// <summary>
    /// Allows to set a new value for every language in the game
    /// </summary>
    /// <param name="newLanguageValues">Array containing all the new values for every language in the game</param>
    public void SetValuesForAllLanguages(IEnumerable<LanguageValue<TValue>> newLanguageValues)
    {
        //validates the received values, making sure they are valid...
        ValidateReceivedNewLanguageValues(newLanguageValues);

        //...before setting them
        languageValues = newLanguageValues.ToArray();
    }

    /// <summary>
    /// Validates the received values, throwing an exception if invalid
    /// </summary>
    /// <param name="newLanguageValues">Values to validate</param>
    private void ValidateReceivedNewLanguageValues(IEnumerable<LanguageValue<TValue>> newLanguageValues)
    {
        Exception exception = null;

        IEnumerable<Language> missingLanguages = GameLanguages.Languages.Where(
            language => !newLanguageValues.Any(value => (value.Language == language))
        )
        .ToArray();
        if (missingLanguages.Any())
        {
            string exceptionMessage;
            if (missingLanguages.Count() == 0)
            {
                exceptionMessage = $"THE NEW LANGUAGE VALUES DO NOT CONTAIN A VALUE FOR THE '{missingLanguages.ElementAt(0)}' LANGUAGE!";
            }
            else
            {
                exceptionMessage = (
                    $"THE NEW LANGUAGE VALUES DO NOT CONTAIN VALUES FOR THE FOLLOWING LANGUAGES:\n" +
                    string.Join("\n", missingLanguages.Select(language => $"- {language}").ToArray())
                );
            }

            exception = new Exception(exceptionMessage + $"\n\n{JsonConvert.SerializeObject(newLanguageValues)}");
        }

        if (exception != null) { throw exception; }
    }

    #endregion

}