using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Abstract class defining how a script that has to handle values for different languages behaves
/// </summary>
/// <typeparam name="TValue"></typeparam>
public abstract class LanguageValuesHandler<TValue>
    : LanguageChangeListener
{
    #region Variables

    [Tooltip("Array containing all the values for every language")]
    [SerializeField]
    protected LanguageValue<TValue>[] languageValues;

    [Tooltip("Array containing all the values for every language from the latest validation")]
    private LanguageValue<TValue>[] validationPreviousLanguageValues = new LanguageValue<TValue>[0];

    #endregion


    #region MonoBehaviour Methods

    protected void OnValidate()
    {
        //makes sure the array is not null
        if (languageValues == null) { languageValues = new LanguageValue<TValue>[0]; }

        //if the array of values doesn't contain enough values for every language in the game...
        int languagesCount = GameLanguages.Languages.Count();
        if (languageValues.Length != languagesCount)
        {
            //...the array is resized to hold the exact amount of languages in the game...
            Array.Resize(ref languageValues, languagesCount);

            //...then, looping through the languages...
            for (int i = 0; i < languagesCount; ++i)
            {
                Language loopedLanguage = (Language)i;

                LanguageValue<TValue> loopedOldValue = validationPreviousLanguageValues.SingleOrDefault(lv => (lv.Language == loopedLanguage));

                //...every value will be set back to the value it had before validation or, if there were none, to its default value...
                languageValues[i] = new LanguageValue<TValue>(
                    name: GameLanguages.GetNameOfLanguage(loopedLanguage),
                    language: loopedLanguage,
                    value: ((loopedOldValue == null) ? GetDefaultValueForLanguage(loopedLanguage) : loopedOldValue.Value)
                );
            }

            //...then, the array containing the values from the latest validation will be updated to copy the ones from this validation
            validationPreviousLanguageValues = languageValues.ToArray();
        }
    }

    #endregion


    /// <summary>
    /// Returns the default value for the desired language
    /// <para>(This is used primarily during validation)</para>
    /// </summary>
    /// <param name="language">Language to get the default value for</param>
    /// <returns></returns>
    protected abstract TValue GetDefaultValueForLanguage(Language language);

}