using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider staminaBar;

    private void Start()
    {
        Player player = Player.Instance;
        player.Health.OnChange += health => hpBar.value = health.CurrentValue / (float)health.Max;
        player.Stamina.OnChange += stamina => staminaBar.value = stamina.CurrentValue / (float)stamina.Max;
    }
}
