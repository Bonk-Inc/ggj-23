using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Guard", menuName = "story/guards/Item Guard")]
public class ItemGuardCondition : DecisionGuard
{
    [SerializeField]
    private ItemType item;

    [SerializeField]
    private int amount = 1;

    public override bool CheckCondition()
    {
        return PlayerInventory.Instance.GetItemCount(item) >= amount;
    }
}
