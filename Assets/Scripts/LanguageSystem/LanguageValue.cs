using System;
using UnityEngine;

/// <summary>
/// Class defining a value for a language present in the game
/// </summary>
/// <typeparam name="TValue">Type of the value used</typeparam>
[Serializable]
public class LanguageValue<TValue>
{

#pragma warning disable IDE0052

    [Tooltip("Name of the language this value is for (used solely as the name displayed in the editor)")]
    [SerializeField]
    [HideInInspector]
    private string Name;

#pragma warning restore


    [Tooltip("Language this value is used for")]
    [HideInInspector]
    public Language Language;


    [Tooltip("Value to use for the defined language")]
    public TValue Value;


    public LanguageValue(string name, Language language, TValue value)
    {
        Name = name;
        Language = language;
        Value = value;
    }
}