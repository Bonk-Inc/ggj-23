using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnHandler : TurnHandler
{

    [SerializeField]
    private Combat combat;

    private bool actionTaken = false;

    [SerializeField]
    private int damage = 40;

    [SerializeField]
    private List<Button> actionButtons;

    private void Awake()
    {
        SetTurnActive(false);
    }

    public override IEnumerator RunTurn()
    {
        SetTurnActive(true);
        actionTaken = false;
        yield return new WaitUntil(() => actionTaken);
        SetTurnActive(false);
    }

    private void SetTurnActive(bool active)
    {
        actionButtons.ForEach((button) => button.interactable = active);
    }

    public void Attack()
    {
        combat.currentEnemy.Hit(damage);
        actionTaken = true;
    }

    public void Flee()
    {
        combat.Flee();
        actionTaken = true;
    }
}
