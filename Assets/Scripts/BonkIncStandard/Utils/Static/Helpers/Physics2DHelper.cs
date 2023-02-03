using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Physics2DHelper
{
    public static Collider2D[] ConeOverlapAll(Vector2 position, Vector2 direction, float coneAngle, float maxDistance = 1, float minDistance = 0) {
        Collider2D[] collidersInCircle = Physics2D.OverlapCircleAll(position, maxDistance);
        List<Collider2D> collidersInCone = new List<Collider2D>();
        foreach (var collider in collidersInCircle) {
            Vector3 colliderPosition = collider.transform.position;
            if (ColliderInCone(colliderPosition, position, direction, coneAngle, minDistance)) {
                collidersInCone.Add(collider);
            }
        }
        return collidersInCone.ToArray();
    }

    private static bool ColliderInCone(Vector3 otherPosition, Vector2 position, Vector2 direction, float coneAngle, float minDistance = 0) {
        if (Vector3.Distance(position, otherPosition) < minDistance) {
            return false;
        }

        Vector2 positionDiff = (Vector2)otherPosition - position;
        float angle = Vector2.Angle(direction, positionDiff);

        if (angle > (coneAngle / 2)) {
            return false;
        }
        return true;
    }
}
