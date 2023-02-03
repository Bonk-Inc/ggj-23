using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberExtensions
{
    public static bool IsInRange(this float f, float min, float max)
    {
        return f >= min && f <= max;
    }
}
