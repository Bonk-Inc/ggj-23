using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class Tag : MultipleChoice
{

    public readonly bool useUnityDefaultTagDropdown;
#if UNITY_EDITOR
    public Tag(bool useUnityDefaultTagDropdown = false) : base(UnityEditorInternal.InternalEditorUtility.tags)
    {
        this.useUnityDefaultTagDropdown = useUnityDefaultTagDropdown;
    }

#endif

}