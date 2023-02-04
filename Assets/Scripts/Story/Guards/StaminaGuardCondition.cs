using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stamina Guard", menuName = "story/guards/Stamina Guard")]
public class StaminaGuardCondition : DecisionGuard
{
    [SerializeField]
    private int stamina = 1;

    public override bool CheckCondition()
    {
        return Player.Instance.Stamina.CurrentValue >= stamina;
    }
}
