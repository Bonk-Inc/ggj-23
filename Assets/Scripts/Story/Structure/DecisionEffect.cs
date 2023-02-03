using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DecisionEffect : ScriptableObject
{
    [SerializeField]
    private string decisionName;

    public abstract void DoEffect(EffectParams effectParams);

}

