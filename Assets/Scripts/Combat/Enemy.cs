using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private EnemyData enemyData;

    [SerializeField]
    private TMPro.TextMeshProUGUI nameUi;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Image healthbar;

    private int health;

    public event Action OnDeath;

    public EnemyData Data => enemyData;

    public void SetEnemyData(EnemyData data)
    {
        OnDeath = null;
        this.enemyData = data;
        health = data.MaxHealth;

        nameUi.text = data.EnemyName;
        image.sprite = data.Image;
        UpdateHealthbar();
    }

    public void Hit(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, enemyData.MaxHealth);
        UpdateHealthbar();
        if (health == 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void UpdateHealthbar()
    {
        healthbar.fillAmount = 1f / enemyData.MaxHealth * health;
    }

}
