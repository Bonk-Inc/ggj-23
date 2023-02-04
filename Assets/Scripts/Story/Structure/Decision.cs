using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision", menuName = "story/decision")]
public class Decision : ScriptableObject
{

    [field: SerializeField]
    public string Title { get; private set; }

    [field: SerializeField]
    public Sprite Image { get; private set; }

    [field: SerializeField]
    public List<DecisionEffect> Effects { get; private set; }

    [field: SerializeField]
    public bool overrideParameters { get; private set; }

    [field: SerializeField]
    public EffectParams Params { get; private set; }

    [field: SerializeField]
    public GamePoint Next { get; private set; }


}
