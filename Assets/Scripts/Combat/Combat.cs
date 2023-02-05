using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private Transform enemyCardParent;

    private Queue<EnemyData> enemyQueue;

    private Action<Outcome, int> onCombatFinished;

    private bool running = false;
    [SerializeField] private Player player;

    [SerializeField] private GameObject combatParent;

    public Enemy currentEnemy;

    public void StartCombat(List<EnemyData> enemies, Action<Outcome, int> OnCombatFinished = null)
    {
        onCombatFinished = OnCombatFinished;
        this.enemyQueue = enemies.ToQueue();
        running = true;
        combatParent.SetActive(true);
        SetNextEnemy();
        int totalDamage = GetTotalDamage();
        while (true)
        {
            //TODO should be damage from player.
            enemyPrefab.Hit(50);

            // not running when all enemies are ded.
            if (!running)
            {
                return;
            }

            totalDamage = GetTotalDamage();
            player.Health.Decrease(totalDamage);

            if (player.Health.CurrentValue <= 0)
            {
                Debug.Log("Ah ohw, no helf left.\nyou lose");
                CombatEnded(Outcome.Lose);
                return;
            }
        }
    }

    private int GetTotalDamage()
    {
        return enemyQueue.Sum(data => data.Damage);
    }


    private void OnEnemyDied()
    {
        enemyQueue.Dequeue();
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
        var next = enemyQueue.Peek();
        //TODO this should spawn the enemy later, but for now it'll just use one in the scene so spawing and such is not nessecary
        enemyPrefab.SetEnemyData(next);
        enemyPrefab.OnDeath += OnEnemyDied;
        currentEnemy = enemyPrefab;
    }


    private void CombatEnded(Outcome outcome)
    {
        if (!running)
            return;

        running = false;
        onCombatFinished?.Invoke(outcome, player.Health.CurrentValue);
        onCombatFinished = null;
        enemyQueue = null;
        currentEnemy = null;
        combatParent.SetActive(false);
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