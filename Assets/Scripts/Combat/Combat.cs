using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private Transform enemyCardParent;

    private Queue<EnemyData> enemyQueue;

    private Action<Outcome> onCombatFinished;

    private bool running = false;

    [SerializeField]
    private GameObject combatParent;

    public Enemy currentEnemy;

    [SerializeField]
    private CombatTurnManager combatTurnManager;

    public void StartCombat(List<EnemyData> enemies, Action<Outcome> OnCombatFinished = null)
    {
        onCombatFinished = OnCombatFinished;
        this.enemyQueue = enemies.ToQueue();
        running = true;
        combatParent.SetActive(true);
        combatTurnManager.StartTurns();
        SetNextEnemy();
    }

    private void OnEnemyDied()
    {
        if (enemyQueue.Count == 0)
        {
            CombatEnded(Outcome.Win);
        }
        else
        {
            SetNextEnemy();
        }
    }

    private void SetNextEnemy()
    {
        var next = enemyQueue.Dequeue();
        //TODO this should spawn the enemy later, but for now it'll just use one in the scene so spawing and such is not nessecary
        enemyPrefab.SetEnemyData(next);
        enemyPrefab.OnDeath += OnEnemyDied;
        currentEnemy = enemyPrefab;
        combatTurnManager.enemyTurn = enemyPrefab.Turnhandler;
    }

    private void CombatEnded(Outcome outcome)
    {
        if (!running)
            return;

        running = false;
        onCombatFinished?.Invoke(outcome);
        onCombatFinished = null;
        enemyQueue = null;
        currentEnemy = null;
        combatParent.SetActive(false);
        combatTurnManager.StopTurns();
    }

    public void Flee()
    {
        CombatEnded(Outcome.Flee);
    }

    public enum Outcome
    {
        Win,
        Lose,
        Flee
    }

}
