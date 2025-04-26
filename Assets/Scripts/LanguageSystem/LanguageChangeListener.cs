using UnityEngine;

/// <summary>
/// Abstract class defining how an object listens to and reacts to when the game's current language changes
/// </summary>
public abstract class LanguageChangeListener
    : MonoBehaviour
{

    [Tooltip("Indicates whether or not this object is currently reacting to the game's current language changing")]
    protected bool reactingToLanguageChange;


    #region MonoBehaviour Methods

    protected virtual void OnEnable()
    {
        //starts listening to whenever the current language changes
        BaseLanguageManager.ListenToLanguageChange(OnLanguageChanged);
    }

    protected virtual void OnDisable()
    {
        //stops listening to whenever the current language changes
        BaseLanguageManager.StopListeningToLanguageChange(OnLanguageChanged);
    }

    #endregion


    #region Language Changing Management

    /// <summary>
    /// Manages what happens when the game's current language changes
    /// </summary>
    /// <param name="language">Indicates the game's current language</param>
    private void OnLanguageChanged(Language language)
    {
        //comunicates this object is reacting to the game's current language changing
        reactingToLanguageChange = true;

        //performs the action to execute whenever the game's current language changes
        DoOnLanguageChanged(language);

        //comunicates this object is no longer reacting to the game's current language changing
        reactingToLanguageChange = false;
    }


    /// <summary>
    /// Action to perform whenever the current language changes
    /// </summary>
    /// <param name="language">Indicates the current language of the game</param>
    protected abstract void DoOnLanguageChanged(Language language);

    #endregion

}