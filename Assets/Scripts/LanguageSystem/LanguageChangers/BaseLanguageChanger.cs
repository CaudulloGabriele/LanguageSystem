using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseLanguageChanger<TObject, TValue>
    : MonoBehaviour
    where TObject : MonoBehaviour
{

    #region Variables

    [SerializeField]
    protected TObject objToChange;

    [SerializeField]
    protected LanguageValue<TValue>[] languageValues;

    #endregion


    #region MonoBehaviour Methods

    private void OnValidate()
    {
        if (languageValues == null) { languageValues = new LanguageValue<TValue>[0]; }

        IEnumerable<Language> languages = GameLanguages.GetLanguages();
        int languagesCount = languages.Count();
        if (languageValues.Length != languagesCount)
        {
            Array.Resize(ref languageValues, languagesCount);

            IEnumerable<string> languagesNames = GameLanguages.GetLanguagesNames();
            for (int i = 0; i < languagesCount; ++i)
            {
                LanguageValue<TValue> loopedValue = languageValues[i];

                Language loopedLanguage = languages.ElementAt(i);

                languageValues[i] = new LanguageValue<TValue>(
                    name: languagesNames.ElementAt(i),
                    language: loopedLanguage,
                    value: ((loopedValue == null) ? GetDefaultValueForLanguage(loopedLanguage) : loopedValue.Value)
                );
            }
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

}