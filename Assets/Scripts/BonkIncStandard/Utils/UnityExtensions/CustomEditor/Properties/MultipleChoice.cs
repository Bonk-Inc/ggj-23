using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MultipleChoice : PropertyAttribute
{

    public readonly List<string> choices; 

    public MultipleChoice(params string[] choices)
    {
        this.choices = new List<string>(choices);
    }
}