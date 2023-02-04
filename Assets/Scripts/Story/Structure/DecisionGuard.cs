using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionGuard : ScriptableObject
{
    public abstract bool CheckGuardCondition();
}
