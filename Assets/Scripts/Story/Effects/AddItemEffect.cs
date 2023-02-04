using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Add Item Effect", menuName = "story/effects/add item")]
public class AddItemEffect : DecisionEffect
{

    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private int amount;

    public override void DoEffect(EffectParams effectParams)
    {
        var addAmount = effectParams?.intValue ?? amount;
        var addItem = effectParams?.itemType ?? itemType;

        PlayerInventory.Instance.InsertItem(itemType, amount);
    }
}
