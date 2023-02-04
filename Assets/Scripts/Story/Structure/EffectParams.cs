using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EffectParams
{
    public int intValue;
    public StoryArea areaValue = StoryArea.None;

    public ItemType itemType = ItemType.Money;

    public GamePoint gamePoint = null;
}
