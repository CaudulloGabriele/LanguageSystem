using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using UnityEngine;

public abstract class LanguageChangeReact<TObject, TValue>
    : MonoBehaviour
    where TObject : MonoBehaviour
{

    #region Variables

    [SerializeField]
    protected TObject objToChange;

    [SerializeField]
    protected LanguageValue<TValue>[] languageValues;

    private LanguageValue<TValue>[] validationPreviousLanguageValues = new LanguageValue<TValue>[0];

    #endregion


    #region MonoBehaviour Methods

    private void OnValidate()
    {
        if (languageValues == null) { languageValues = new LanguageValue<TValue>[0]; }

        int languagesCount = GameLanguages.Languages.Count();
        if (languageValues.Length != languagesCount)
        {
            Array.Resize(ref languageValues, languagesCount);

            for (int i = 0; i < languagesCount; ++i)
            {
                Language loopedLanguage = (Language)i;

                LanguageValue<TValue> loopedOldValue = validationPreviousLanguageValues.SingleOrDefault(lv => (lv.Language == loopedLanguage));

                languageValues[i] = new LanguageValue<TValue>(
                    name: GameLanguages.GetNameOfLanguage(loopedLanguage),
                    language: loopedLanguage,
                    value: ((loopedOldValue == null) ? GetDefaultValueForLanguage(loopedLanguage) : loopedOldValue.Value)
                );
            }

            validationPreviousLanguageValues = languageValues.ToArray();
        }
    }


    private void Awake()
    {
        BaseLanguageManager.ListenToLanguageChange(ChangeLanguageTo);
    }

    private void OnDestroy()
    {
        BaseLanguageManager.StopListeningToLanguageChange(ChangeLanguageTo);
    }

    #endregion


    #region Language Change Management

    public void ChangeLanguageTo(Language language)
        => ManageObjectChangeOnLanguageChange(languageValues.Single(lv => (lv.Language == language)).Value);
    

    protected abstract void ManageObjectChangeOnLanguageChange(TValue newValue);

    protected abstract TValue GetDefaultValueForLanguage(Language language);

    #endregion

    #region Languages Value Management

    protected TValue GetValueForLanguage(Language language)
        => languageValues.Single(lv => (lv.Language == language)).Value;

    public void SetValueForLanguage(Language language, TValue newValue)
    {
        bool valueSetCorrectly = false;
        foreach (LanguageValue<TValue> loopedLanguageValue in languageValues)
        {
            if (loopedLanguageValue.Language == language)
            {
                loopedLanguageValue.Value = newValue;
                valueSetCorrectly = true;

                break;
            }
        }

        if (!valueSetCorrectly)
        {
            string exceptionMessage = "THE VALUE WAS NOT SET CORRECTLY, BECAUSE ";
            if (!GameLanguages.Languages.Contains(language)) { exceptionMessage += $"THE RECEIVED LANGUAGE IS NOT MANAGED: {language}"; }
            else if (languageValues.All(lv => (lv.Language != language)))
            {
                exceptionMessage += $"THE ARRAY OF VALUES FOR LANGUAGES DOES NOT CONTAIN A VALUE FOR THIS LANGUAGE: {language}";
            }
            else { exceptionMessage += "OF AN UNEXPECTED!"; }

            throw new Exception(exceptionMessage);
        }
    }

    public void SetValuesForAllLanguages(IEnumerable<LanguageValue<TValue>> newLanguageValues)
    {
        ValidateReceivedNewLanguageValues(newLanguageValues);

        languageValues = newLanguageValues.ToArray();
    }

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