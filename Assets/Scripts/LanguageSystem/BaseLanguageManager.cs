using UnityEngine;

public abstract class BaseLanguageManager
    : MonoBehaviour
{

    #region Delegates

    public delegate void OnLanguageChange(Language language);

    private OnLanguageChange onLanguageChange;


    public static void ListenToLanguageChange(OnLanguageChange listener)
        => Instance.onLanguageChange += listener;

    public static void StopListeningToLanguageChange(OnLanguageChange listener)
        => Instance.onLanguageChange -= listener;

    #endregion


    #region Variables

    private static BaseLanguageManager _inst;

    public static BaseLanguageManager Instance
    {
        get
        {
            //if the instance was not set and we are in the editor that is not playing, 
            if ((_inst == null) && Application.isEditor && !Application.isPlaying) { Instance = FindObjectOfType<BaseLanguageManager>(); }

            return _inst;
        }

        private set {  _inst = value; }
    }


    private Language currentLanguage = Language.English;

    #endregion


    #region MonoBehaviour Methods

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        ChangeLanguage(GetSavedLanguage());
    }

    private void OnDestroy()
    {
        onLanguageChange = null;

        SaveCurrentLanguage();
    }

    #endregion


    #region Overridable Methods

    public abstract Language GetSavedLanguage();

    public abstract void SaveCurrentLanguage();

    #endregion


    #region Language Change Management

    public static void ChangeLanguage(Language newLanguage)
    {
        Instance.currentLanguage = newLanguage;

        Instance.onLanguageChange?.Invoke(newLanguage);
    }

    #endregion


    public static Language GetCurrentLanguage()
        => Instance.currentLanguage;

}