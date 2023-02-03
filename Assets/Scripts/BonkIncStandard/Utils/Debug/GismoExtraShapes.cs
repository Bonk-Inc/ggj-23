using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GismoExtraShapes
{
    public static void DrawCircleCone2D(Vector3 position, Vector3 direction, float maxDistance, float minDistance, float angle)
    {
        var normalizedDirection = direction.normalized;

        Gizmos.DrawWireSphere(position, maxDistance);
        Gizmos.DrawWireSphere(position, minDistance);

        var positiveRotatedDirection = Quaternion.Euler(0, 0, angle / 2) * normalizedDirection;
        var negativeRotatedDirection = Quaternion.Euler(0, 0, -angle / 2) * normalizedDirection;

        Gizmos.DrawLine(position + positiveRotatedDirection * minDistance, position + positiveRotatedDirection * maxDistance);
        Gizmos.DrawLine(position + negativeRotatedDirection * minDistance, position + negativeRotatedDirection * maxDistance);
    }

    public static void DrawUnitSphere(Vector3 center, Color? color = null)
    {
        var prefColor = Gizmos.color;
        var drawColor = color ?? prefColor;
        Gizmos.color = drawColor;
        Gizmos.DrawWireSphere(center, 1);
        Gizmos.color = prefColor;

    }
}
