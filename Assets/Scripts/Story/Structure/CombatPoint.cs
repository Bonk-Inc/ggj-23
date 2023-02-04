using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combat Point", menuName = "story/story points/combat point")]
public class CombatPoint : GamePoint
{

    [field: SerializeField]
    public List<EnemyData> Enemies { get; private set; }

    [field: SerializeField]
    public GamePoint NextWin { get; private set; }

    [field: SerializeField]
    public GamePoint NextFlee { get; private set; }

    [field: SerializeField]
    public GamePoint NextLose { get; private set; }

}
