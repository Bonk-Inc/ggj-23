using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDecisionEffect : DecisionEffect
{

    [SerializeField]
    private int change;

    public override void DoEffect(EffectParams effectParams)
    {
        var healthEffect = EffectsHandler.GetEffect(Effect.HealthChange);
        healthEffect.Invoke(effectParams ?? new()
        {
            intValue = change
        });
    }

}
