using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "combat/enemy")]
public class EnemyData : ScriptableObject
{

    [field: SerializeField]
    public string EnemyName { get; private set; }

    [field: SerializeField]
    public Sprite Image { get; private set; }

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public int MaxHealth { get; private set; }

}
