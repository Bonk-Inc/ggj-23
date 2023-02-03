using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extention
{
    public static Vector3 ToVector3(this Vector2 vector2, float z = 0) {
        var vector3 = (Vector3)vector2;
        vector3.z = z;
        return vector3;
    }

    public static Vector2 Rotate(this Vector2 v, float degrees) {
         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }
}