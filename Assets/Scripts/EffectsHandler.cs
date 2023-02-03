using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{

    [SerializeField]
    private Player player;

    private static Dictionary<Effect, Action<EffectParams>> effects;

    private void Awake()
    {
        effects = new() {
            { Effect.HealthChange, (param) => { player.Health -= param.intValue; } },
            { Effect.StatminaChange, (param) => { player.Stamina -= param.intValue; } },
        };
    }

    public static Action<EffectParams> GetEffect(Effect effect)
    {
        return effects[effect];
    }

}
