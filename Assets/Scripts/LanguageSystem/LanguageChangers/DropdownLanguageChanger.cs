using System.Collections.Generic;
using System.Linq;
using TMPro;

/// <summary>
/// Class defining how a dropdown that allows to change the game's current language behaves
/// </summary>
public class DropdownLanguageChanger
    : LanguageChanger<TMP_Dropdown, int>
{

    #region LanguageChanger Methods

    protected override int GetDefaultValueForLanguage(Language language)
        => (int)language;

    protected override int GetObjectCurrentValue()
        => objChanger.value;

    protected override void InitializeObject(Language currentLanguage, IEnumerable<Language> allLanguages)
    {
        //makes sure the dropdown has only the correct options for every language in the game
        objChanger.ClearOptions();
        objChanger.AddOptions(
            allLanguages.Select(language => new TMP_Dropdown.OptionData(GameLanguages.GetNameOfLanguage(language))).ToList()
        );

        //makes sure that, whenever the value of this dropdown changes, the game's current language will change based on that value
        objChanger.onValueChanged.AddListener((_) => ChangeCurrentLanguageToObjectCurrentValue());
    }

    protected override void UpdateObject(Language language)
        => objChanger.value = objChanger.options.FindIndex(o => (o.text == GameLanguages.GetNameOfLanguage(language)));

    #endregion

}