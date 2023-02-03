using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsCalculationHelper
{

    public static Bounds CalculateBounds2D(Collider2D[] colliders)
    {
        Bounds[] bounds = new Bounds[colliders.Length];

        for (int i = 0; i < colliders.Length; i++)
        {
            bounds[i] = colliders[i].bounds;
        }

        return CalculateBounds2D(bounds);
    }

    public static Bounds CalculateBounds2D(Renderer[] renderers)
    {
        Bounds[] bounds = new Bounds[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            bounds[i] = renderers[i].bounds;
        }

        return CalculateBounds2D(bounds);
    }

    public static Bounds CalculateBounds2D(Collider2D[] colliders, Renderer[] renderers)
    {
        List<Bounds> bounds = new List<Bounds>
        {
            CalculateBounds2D(colliders),
            CalculateBounds2D(renderers)
        };
        return CalculateBounds2D(bounds.ToArray());
    }

    public static Bounds CalculateBounds2D(Bounds[] allBounds)
    {
        if (allBounds == null || allBounds.Length == 0)
            return new Bounds();

        if (allBounds.Length == 1)
            return allBounds[0];

        Bounds combinedBounds = new Bounds();
        for (int i = 0; i < allBounds.Length; i++)
        {
            combinedBounds.Encapsulate(allBounds[i]);
        }
        return combinedBounds;

    }


}