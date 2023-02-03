using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extention
{
    public static Vector3 Multiply(this Vector3 vector3, Vector3 multiplier)
    {
        vector3.Scale(multiplier);
        return vector3;
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return (Vector2)v;
    }
}