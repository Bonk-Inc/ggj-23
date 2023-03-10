using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectsHandler : MonoBehaviour
{

    [SerializeField]
    private Player player;

    [SerializeField]
    private StoryPlayer story;

    private static Dictionary<Effect, Action<EffectParams>> effects;

    private void Awake()
    {
        effects = new() {
            { Effect.HealthChange, (param) => { player.Health.Decrease(param.intValue); } },
            { Effect.StatminaChange, (param) => { player.Stamina.Decrease(param.intValue); } },
            { Effect.ChangeArea, (param) => { story.Area = param.areaValue; } },
        };
    }

    public static Action<EffectParams> GetEffect(Effect effect)
    {
        return effects[effect];
    }

}
