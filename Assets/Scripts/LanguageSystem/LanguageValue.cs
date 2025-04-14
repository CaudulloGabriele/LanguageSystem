using System;
using UnityEngine;

[Serializable]
public class LanguageValue<TValue>
{
    [HideInInspector]
    public string Name;

    [HideInInspector]
    public Language Language;

    public TValue Value;


    public LanguageValue(string name, Language language, TValue value)
    {
        Name = name;
        Language = language;
        Value = value;
    }

}