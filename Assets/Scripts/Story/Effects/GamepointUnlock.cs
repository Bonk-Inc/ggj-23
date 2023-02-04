using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepointUnlock : DecisionEffect
{

    [SerializeField]
    private StoryArea area;

    [SerializeField]
    private GamePoint unlock;

    public override void DoEffect(EffectParams effectParams)
    {
        var unlockArea = effectParams?.areaValue ?? area;
        var pointUnlock = effectParams?.gamePoint ?? unlock;
        RunStorage.Instance.Unlock(unlockArea, pointUnlock);
    }
}
