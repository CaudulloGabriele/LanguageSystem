using UnityEngine;

/// <summary>
/// Abstract class defining how an object that has to manage the game's language behaves
/// </summary>
public abstract class BaseLanguageManager
    : MonoBehaviour
{

    #region Delegates

    /// <summary>
    /// Delegate defining listeners to whenever the game's current language changes
    /// </summary>
    /// <param name="language"></param>
    public delegate void OnLanguageChange(Language language);

    /// <summary>
    /// Contains all the listeners to whenever the game's current language changes
    /// </summary>
    private static OnLanguageChange onLanguageChange;


    /// <summary>
    /// Allows to add a listener to whenever the game's current language changes
    /// </summary>
    /// <param name="listener"></param>
    public static void ListenToLanguageChange(OnLanguageChange listener)
        => onLanguageChange += listener;

    /// <summary>
    /// Allows to remove a listener to whenever the game's current language changes
    /// </summary>
    /// <param name="listener"></param>
    public static void StopListeningToLanguageChange(OnLanguageChange listener)
        => onLanguageChange -= listener;

    #endregion


    #region Variables

    /// <summary>
    /// Indicates the game's current language
    /// </summary>
    private static Language currentLanguage = 0;

    #endregion


    #region MonoBehaviour Methods

    private void Start()
    {
        /*
         * as soon as the game starts, right after any listener has had the chance to start listening,
         * changes the game's current language to the language that was previously saved
        */
        ChangeLanguage(GetSavedLanguage());
    }

    private void OnDestroy()
    {
        //removes every listener to whenever the game's current language changes
        onLanguageChange = null;

        //saves the current language, so it will be used next time the game is opened
        SaveCurrentLanguage(GetCurrentLanguage());
    }

    #endregion


    #region Overridable Methods

    /// <summary>
    /// Returns the saved language
    /// </summary>
    /// <returns></returns>
    public abstract Language GetSavedLanguage();

    /// <summary>
    /// Saves the current language
    /// <param name="currentLanguage">Indicates the game's current language to save</param>
    /// </summary>
    public abstract void SaveCurrentLanguage(Language currentLanguage);

    #endregion


    #region Language Change Management

    /// <summary>
    /// Allows to change the game's current language
    /// </summary>
    /// <param name="newLanguage">Language to change to</param>
    public static void ChangeLanguage(Language newLanguage)
    {
        //updates the value of the game's current language...
        currentLanguage = newLanguage;

        //...and tells every listener the game's current language changed
        onLanguageChange?.Invoke(newLanguage);
    }

    #endregion


    /// <summary>
    /// Returns the game's current language
    /// </summary>
    /// <returns></returns>
    public static Language GetCurrentLanguage()
        => currentLanguage;

}