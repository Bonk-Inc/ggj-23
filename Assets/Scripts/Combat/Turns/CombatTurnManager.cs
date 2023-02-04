using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTurnManager : MonoBehaviour
{

    [SerializeField]
    private Combat combat;

    [field: SerializeField]
    public TurnHandler PlayerTurn { get; private set; }

    public TurnHandler enemyTurn;

    private Coroutine turnsRoutine;

    public void StartTurns()
    {
        turnsRoutine = StartCoroutine(RunTurns());
    }

    public void StopTurns()
    {
        StopCoroutine(turnsRoutine);
    }

    private IEnumerator RunTurns()
    {
        bool player = true;
        while (true)
        {
            yield return StartCoroutine(player ? PlayerTurn.RunTurn() : enemyTurn.RunTurn());
            player = !player;
        }

    }

}
