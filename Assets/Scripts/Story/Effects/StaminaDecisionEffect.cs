using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stamina Effect", menuName = "story/effects/stamina")]
public class StaminaDecisionEffect : DecisionEffect
{

    [SerializeField]
    private int change;

    public override void DoEffect(EffectParams effectParams)
    {
        var staminaEffect = EffectsHandler.GetEffect(Effect.StatminaChange);
        staminaEffect.Invoke(effectParams ?? new()
        {
            intValue = change
        });
    }

}
