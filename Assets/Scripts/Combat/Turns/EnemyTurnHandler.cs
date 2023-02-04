using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnHandler : TurnHandler
{

    [SerializeField]
    private Player player;

    [SerializeField]
    private Enemy enemy;

    public override IEnumerator RunTurn()
    {
        //TODO maybe some cool effects on the cards OwO
        yield return new WaitForSeconds(0.5f);
        player.Health.Decrease(enemy.Data.Damage);
    }
}
