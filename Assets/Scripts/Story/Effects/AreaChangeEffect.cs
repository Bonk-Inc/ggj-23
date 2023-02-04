using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Area Effect", menuName = "story/effects/area")]
public class AreaChangeEffect : DecisionEffect
{
    [SerializeField]
    private StoryArea area;

    public override void DoEffect(EffectParams effectParams)
    {
        var effectHandler = EffectsHandler.GetEffect(Effect.ChangeArea);
        effectHandler.Invoke(effectParams ?? new()
        {
            areaValue = area
        });
    }
}
