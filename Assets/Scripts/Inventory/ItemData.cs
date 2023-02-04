using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "item")]
public class ItemData : ScriptableObject
{

    [field: SerializeField]
    public ItemType ItemType { get; private set; }

    [field: SerializeField]
    public string ItemName { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    public int Max { get; private set; }

    [field: SerializeField]
    public bool IsTool { get; private set; } = false;

}
