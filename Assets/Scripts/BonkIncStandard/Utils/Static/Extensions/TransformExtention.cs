using UnityEngine;

public static class TransformExtensions
{
    public static void setPositionX(this Transform transform, float x)
    {
        var position = transform.position;
        position.x = x;
        transform.position = position;
    }

    public static void setPositionY(this Transform transform, float y)
    {
        var position = transform.position;
        position.y = y;
        transform.position = position;
    }

    public static void setPositionZ(this Transform transform, float z)
    {
        var position = transform.position;
        position.z = z;
        transform.position = position;
    }

    public static void setLocalX(this Transform transform, float x)
    {
        var position = transform.localPosition;
        position.x = x;
        transform.localPosition = position;
    }

    public static void setLocalY(this Transform transform, float y)
    {
        var position = transform.localPosition;
        position.y = y;
        transform.localPosition = position;
    }

    public static void setLocalZ(this Transform transform, float z)
    {
        var position = transform.localPosition;
        position.z = z;
        transform.localPosition = position;
    }

    public static void setLocalScaleX(this Transform transform, float x)
    {
        var scale = transform.localScale;
        scale.x = x;
        transform.localScale = scale;
    }

    public static void setLocalScaleY(this Transform transform, float y)
    {
        var scale = transform.localScale;
        scale.y = y;
        transform.localScale = scale;
    }

    public static void setLocalScaleZ(this Transform transform, float z)
    {
        var scale = transform.localScale;
        scale.z = z;
        transform.localScale = scale;
    }
}
