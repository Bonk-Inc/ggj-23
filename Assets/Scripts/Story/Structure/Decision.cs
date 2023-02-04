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
    public List<DecisionGuard> Guards { get; private set; }

    [field: SerializeField]
    public List<InnerDecisionEffect> Effects { get; private set; }

    [field: SerializeField]
    public GamePoint Next { get; private set; }

    [field: SerializeField]
    public Explainer ExplainingPoint { get; private set; }


    [System.Serializable]
    public class InnerDecisionEffect
    {

        [field: SerializeField]
        public bool OverrideParams;

        [field: SerializeField]
        public EffectParams Params { get; private set; }

        [field: SerializeField]
        public DecisionEffect effect { get; private set; }

    }



}


[System.Serializable]
public class Explainer
{
    [field: SerializeField]
    public string Title { get; private set; } = string.Empty;

    [field: SerializeField]
    public string Explaination { get; private set; } = string.Empty;
}