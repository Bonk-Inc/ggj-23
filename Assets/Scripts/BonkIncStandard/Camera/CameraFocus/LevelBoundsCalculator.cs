using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundsCalculator : MonoBehaviour
{

    [SerializeField]
    private Collider2D[] colliders;

    [SerializeField]
    private Renderer[] renderers;

    public Bounds currentLevelBounds { get; private set; }

    public event Action<Bounds> OnBoundsUpdated;

    private void Awake()
    {
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        currentLevelBounds = BoundsCalculationHelper.CalculateBounds2D(colliders, renderers);
        OnBoundsUpdated?.Invoke(currentLevelBounds);
    }

}