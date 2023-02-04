using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionGuard : ScriptableObject
{

    [SerializeField]
    private bool reverseCondigion;

    public bool CheckGuardCondition()
    {
        if (reverseCondigion)
        {
            return !CheckCondition();
        }
        else
        {
            return CheckCondition();
        }
    }
    public abstract bool CheckCondition();
}
